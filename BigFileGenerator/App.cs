using Microsoft.Extensions.Configuration;

namespace BigFileGenerator;

public class App
{
    private readonly IConfiguration _config;
    private readonly string _inputPath;
    private readonly string _outputPath;
    private readonly int _maxLines;
    private readonly string _columnSeparator;
    private readonly int _maxCol1Number;
    private readonly int _minWordsPerLine;
    private readonly int _maxWordsPerLine;

    public App(IConfiguration config)
    {
        _config = config;
        _inputPath = config.GetSection("RandomTextFileOptions").GetValue<string>("Path") ?? string.Empty;
        _outputPath = config.GetSection("OutputFileOptions").GetValue<string>("Path") ?? string.Empty;
        _maxLines = config.GetSection("OutputFileOptions").GetValue<int>("MaxLines");
        _columnSeparator = config.GetSection("OutputFileOptions").GetValue<string>("ColumnSeparator") ?? ". ";
        _maxCol1Number = config.GetSection("OutputFileOptions").GetValue<int>("MaxCol1Number");
        _minWordsPerLine = config.GetSection("OutputFileOptions").GetValue<int>("MinWordsPerLine");
        _maxWordsPerLine = config.GetSection("OutputFileOptions").GetValue<int>("MaxWordsPerLine");
    }

    public async Task Run()
    {
        var rnd = new Random();
        try
        {
            var inPath = string.Concat(Directory.GetCurrentDirectory(), _inputPath);
            var sourceFileReader = new SourceFileReader(inPath);
            var outPath = string.Concat(Directory.GetCurrentDirectory(), _outputPath);
            var outputFileWriter = new OutputFileWriter(outPath);
            try
            {
                Console.WriteLine($"{DateTime.Now:hh:mm:ss} --- Generating test file ---");
                var marker = _maxLines / 100;
                for (var i = 0; i < _maxLines; i++)
                {
                    var sentence = string.Concat(rnd.Next(_maxCol1Number), _columnSeparator,
                        sourceFileReader.GetRandomSentence(rnd, rnd.Next(_minWordsPerLine, _maxWordsPerLine)));
                    await outputFileWriter.WriteOneLineAsync(sentence);
                    if (i % marker == 0)
                    {
                        Console.Write("*");
                    }
                }
                await outputFileWriter.FlushAll();
                var fileInfo = new FileInfo(outPath);
                Console.WriteLine();
                Console.WriteLine($"{DateTime.Now:hh:mm:ss} Test file's generated: {_maxLines} lines, size: {fileInfo.Length / 1024 / 1024} MB");
            }
            finally
            {
                sourceFileReader.Dispose();
                outputFileWriter.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}, {ex.StackTrace}");
        }
    }
}