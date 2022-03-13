using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileReaderWriter.Extensions;
using FileReaderWriter.ReadOptions;
using static FileReaderWriter.Enums.ArgumentEnum;

namespace FileReaderWriter.CommandLineOperations
{
    public class TextSearcher
    {
        readonly char[] delimiterChars = { ' ', ',', '.', ':', ';', '!', '?', '\t', '\r', '\n', '—', '-', '"' };

        public async Task SearchTextInFile(string[] args)
        {
            string searchWord = null;
            string sourceText = null;
            string sourceFilePath = null;
            var allowedArguments = new List<Argument> { Argument.search, Argument.shift, Argument.direction, Argument.source };

            foreach (var arg in args)
            {
                if (arg.Contains(Argument.search.ToValidArgument()))
                {
                    searchWord = GetSearchWord(arg);                       
                }
                if (arg.Contains(Argument.source.ToValidArgument()))
                {
                    sourceFilePath = PathReader.GetPathFromCommandLineArgument(arg);

                    sourceText = await CommandLineReader.GetTextFromSourceFileAsync(sourceFilePath, args);
                }
                else
                {
                    if (!allowedArguments.Any(allowedArg => arg.Contains(allowedArg.ToValidArgument())))
                            throw new ArgumentException($"Wrong {arg} argument");
                }
            }

            PrintNumberOfOccurences(searchWord, sourceText);
        }

        private void PrintNumberOfOccurences(string searchWord, string sourceText)
        {
            List<string> splitText = sourceText.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).ToList();

            int numberOfOccurences = splitText.Where(word => word.Equals(searchWord)).Count();

            Console.WriteLine($"The word \"{searchWord}\" appears in the text {numberOfOccurences} time(-s)");
        }

        private string GetSearchWord(string searchArg)
        {
            int wordIndex = searchArg.IndexOf('=') + 1;

            string searchWord = searchArg.Substring(wordIndex).Trim();

            if (searchWord != null)
            {
                return searchWord;
            }

            throw new ArgumentException("Search word was null");
        }
    }
}