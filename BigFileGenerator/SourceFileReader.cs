using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BigFileGenerator;

public class SourceFileReader: IDisposable
{
    private IList<string> _wordList;
    private string _content;

    public SourceFileReader(string path)
    {
        _content = File.ReadAllText(path);
        _wordList = _content
            .Split(new[] { ' ', '\n', ',', '.' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
    }

    public string GetRandomSentence(Random rnd, int numberOfWords)
    {
        var resultList = new List<string>();
        for (var i = 0; i < numberOfWords; i++)
        {
            var randomIndex = rnd.Next(_wordList.Count);
            resultList.Add(_wordList[randomIndex]);
        }
        return string.Join(' ', resultList);
    }

    public void Dispose()
    {
        _wordList.Clear();
    }
}