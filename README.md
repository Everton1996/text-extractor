# PDF & HTML Text Extractor

Este é um aplicativo de console em C# que processa arquivos `.pdf` e `.html` localizados em uma pasta `Input`, extrai o texto de cada arquivo e salva o conteúdo em arquivos `.txt` na pasta `Output`. Ao final, é exibido um relatório de desempenho e erros de processamento.

## Requisitos

- [.NET SDK 6.0 ou superior](https://dotnet.microsoft.com/download)
- Visual Studio, Visual Studio Code ou outro editor compatível.

## Dependências

Este projeto usa os seguintes pacotes NuGet:

- `UglyToad.PdfPig` — para leitura de PDFs
- `HtmlAgilityPack` — para análise de arquivos HTML

Para instalar as dependências, execute:

```bash
dotnet add package UglyToad.PdfPig
dotnet add package HtmlAgilityPack
```

## Estrutura esperada de pastas

Antes de executar, crie uma pasta chamada `Input` no mesmo diretório onde está o executável ou os arquivos `.cs`, e coloque os arquivos `.pdf` e `.html` que deseja processar ali.

```text
SeuProjeto/
├── Input/
│   ├── arquivo1.pdf
│   └── arquivo2.html
├── Output/         (gerada automaticamente)
├── Program.cs
└── ...
```

## Compilação

Para compilar o projeto via terminal:

```bash
dotnet build
```

## Execução

Para rodar o programa:

```bash
dotnet run
```

Ao final da execução, os arquivos `.txt` extraídos estarão na pasta `Output`.

## Relatório

O programa imprime um relatório no console com informações como:

- Total de arquivos processados com sucesso e com erro
- Tempo médio de processamento por tipo de arquivo
- Tempo total de execução

## Observações

- Certifique-se de que os arquivos na pasta `Input` tenham extensões `.pdf` ou `.html`.
- Arquivos com erros de leitura serão registrados no relatório, mas não interromperão o processamento dos demais arquivos.
