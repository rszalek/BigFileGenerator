namespace BigFileGenerator;

public class SourceFileReader: IDisposable
{
    private readonly IList<string> _wordList;

    public SourceFileReader(string path)
    {
        var content = File.ReadAllText(path);
        _wordList = content
            .Split(new[] { ' ', '\r', '\n', ',', '.' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
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