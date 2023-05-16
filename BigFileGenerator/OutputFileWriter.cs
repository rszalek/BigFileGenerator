using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BigFileGenerator;

public record OutputTextRecord(int Number, string Sentence);

public class OutputFileWriter: IDisposable
{
    private readonly StreamWriter _streamWriter;

    public OutputFileWriter(string path)
    {
        _streamWriter = File.CreateText(path);
    }

    public async Task WriteOneLineAsync(string text)
    {
        await _streamWriter.WriteLineAsync(text);
    }

    public async Task FlushAll()
    {
        await _streamWriter.FlushAsync();
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
    }
}