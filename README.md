# BigFileGenerator
## Big text file generator for testing.

This is a tool application to generate large random data file, where each line is of the form Number. String
For example:
```
415. Apple
30432. Something something something
1. Apple
32. Cherry is the best
2. Banana is yellow
```
Both parts can be repeated within the file.

Type: **Console app.**

Framework: **.NET7**

Configuration file: **appsettings.json**

### Configuration parameters' description:

```
{
    "RandomTextFileOptions": {
        "Path": "\\InputFiles\\Source1.txt"     -- file with randomly generated sentences, no NewLine characters 
    },
    "OutputFileOptions": {
        "Path": "\\OutputFiles\\TestFile.txt",  -- path for output test file 
        "MaxLines": 50000000,                   -- number of lines to be generated
        "ColumnSeparator": ". ",                -- column separator for the test file
        "MaxCol1Number": 999999,                -- max integer number used in column 1 
        "MinWordsPerLine": 1,                   -- minimum count of words used to generate column 2 text
        "MaxWordsPerLine": 10                   -- maximum count of words used to generate column 2 text
    }
}
```
