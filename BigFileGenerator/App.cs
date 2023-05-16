using Microsoft.Extensions.Configuration;

namespace BigFileGenerator;

public class App
{
    private readonly IConfigurationRoot _config;
    private string inputPath = "";
    private string outputPath = "";
    private TextReader inputFile;

    public App(IConfigurationRoot config)
    {
        _config = config;
        var inputPathValue = config.GetSection("InputFileOptions:Path").Value;
        if (inputPathValue != null) inputPath = inputPathValue;
        var outputPathValue = config.GetSection("OutputFileOptions:Path").Value;
        if (outputPathValue != null) outputPath = outputPathValue;
    }

    public async Task Run(int times)
    {
        var rnd = new Random();
        try
        {
            var inPath = string.Concat(Directory.GetCurrentDirectory(), inputPath);
            var outPath = string.Concat(Directory.GetCurrentDirectory(), outputPath);
            var sourceFileReader = new SourceFileReader(inPath);
            var outputFileWriter = new OutputFileWriter(outPath);
            try
            {
                for (var i = 0; i < times; i++)
                {
                    var sentence = string.Concat(rnd.Next(999999), ". ", sourceFileReader.GetRandomSentence(rnd, rnd.Next(1, 10)));
                    await outputFileWriter.WriteOneLineAsync(sentence);
                    //Console.WriteLine(sentence);
                }
                await outputFileWriter.FlushAll();
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