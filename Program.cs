using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using UglyToad.PdfPig;
using HtmlAgilityPack;

class Program
{
    static void Main()
    {
        var totalTimer = Stopwatch.StartNew();

        var inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        var outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputFolder);

        var files = Directory.GetFiles(inputFolder, ".", SearchOption.AllDirectories).Where(
            f => f.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".html", StringComparison.OrdinalIgnoreCase)).ToList();

        var report = new ProcessingReport();

        Parallel.ForEach(files, file =>
        {
            var text = string.Empty;
            var extension = Path.GetExtension(file);
            var timer = Stopwatch.StartNew();
            var isPdf = extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

            try
            {
                text = isPdf ? ExtractTextFromPdf(file) : ExtractTextFromHtml(file);
                
                if (isPdf) report.AddPdfSuccess(timer.Elapsed.TotalSeconds);
                else report.AddHtmlSuccess(timer.Elapsed.TotalSeconds);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    var outputPath = Path.Combine(outputFolder, $"{Path.GetFileNameWithoutExtension(file)}.txt");
                    File.WriteAllText(outputPath, text);
                }               

            }
            catch (Exception ex)
            {
                timer.Stop();
                if (extension == ".pdf")
                    report.IncrementPdfError();
                else if (extension == ".html")
                    report.IncrementHtmlError();

                Console.WriteLine($"\nErro ao processar {Path.GetFileName(file)}: {ex.Message}");
            }
        });

        totalTimer.Stop();
        report.TotalProcessingTime = totalTimer.Elapsed.TotalSeconds;
        report.PrintSummary();

    }

    static string ExtractTextFromPdf(string path)
    {
        using var document = PdfDocument.Open(path);
        return string.Join(Environment.NewLine, document.GetPages().Select(p => p.Text));
    }

    static string ExtractTextFromHtml(string path)
    {
        var doc = new HtmlDocument();
        doc.Load(path);
        return doc.DocumentNode.InnerText;
    }
}