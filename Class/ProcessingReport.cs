public class ProcessingReport
{
    private int _pdfSuccess, _htmlSuccess, _pdfError, _htmlError = 0;
    private double _totalPdfTime, _totalHtmlTime = 0;

    public double TotalProcessingTime { get; set; }

    private object lockObj = new object();


    public void AddPdfSuccess(double elapsedSeconds)
    {
        lock (lockObj)
        {
            _pdfSuccess++;
            _totalPdfTime += elapsedSeconds;
        }
    }

    public void AddHtmlSuccess(double elapsedSeconds)
    {
        lock (lockObj)
        {
            _htmlSuccess++;
            _totalHtmlTime += elapsedSeconds;
        }
    }

    public void IncrementPdfError()
    {
        Interlocked.Increment(ref _pdfError);
    }

    public void IncrementHtmlError()
    {
        Interlocked.Increment(ref _htmlError);
    }

    public void PrintSummary()
    {
        Console.WriteLine("\n\t-------- RELATÓRIO FINAL --------");
        Console.WriteLine($"\nTempo total de processamento: {TotalProcessingTime:F2} segundos");
        Console.WriteLine($"Total de PDFs com sucesso: {_pdfSuccess} | Erros: {_pdfError}");
        Console.WriteLine($"Total de HTMLs com sucesso: {_htmlSuccess} | Erros: {_htmlError}");

        double pdfAvg = _pdfSuccess > 0 ? _totalPdfTime / _pdfSuccess : 0;
        double htmlAvg = _htmlSuccess > 0 ? _totalHtmlTime / _htmlSuccess : 0;

        Console.WriteLine($"Tempo médio por PDF: {pdfAvg:F2} segundos");
        Console.WriteLine($"Tempo médio por HTML: {htmlAvg:F2} segundos");
    }
}
