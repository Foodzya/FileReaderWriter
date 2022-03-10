using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileReaderWriter.Extensions;
using FileReaderWriter.ReadOptions;
using FileReaderWriter.TextManipulations;
using static FileReaderWriter.Enums.ArgumentEnum;
using static FileReaderWriter.Enums.FileFormatEnum;

namespace FileReaderWriter.CommandLineOperations
{
    public class TextSearcher
    {
        public async Task SearchTextInFile(string[] args)
        {
            string searchWord = null;

            string sourceText = null;

            List<Argument> allowedArguments = new List<Argument> { Argument.search, Argument.shift, Argument.direction, Argument.source };

            foreach (var arg in args)
            {
                if (arg.Contains(Argument.search.ToValidArgument()))
                {
                    searchWord = GetSearchWord(arg);                       
                }
                if (arg.Contains(Argument.source.ToValidArgument()))
                {
                    int sourcePathIndex = arg.IndexOf('=') + 1;

                    string sourceFilePath = arg.Substring(sourcePathIndex);

                    sourceText = await GetTextFromSourceFile(sourceFilePath, args);
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
            char[] delimiterChars = { ' ', ',', '.', ':', ';', '\t', '\r', '\n', '—', '-', '"' };

            List<string> splitText = sourceText.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).ToList();

            int numberOfOccurences = splitText.Where(word => word.Equals(searchWord)).Count();

            Console.WriteLine($"Word \"{searchWord}\" appears in the text {numberOfOccurences} times");
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

        private async Task<string> GetTextFromSourceFile(string sourceFilePath, string[] args)
        {
            string format = Path.GetExtension(sourceFilePath);

            string formattedTextFromSourceFile;

            FileReader fileReader = new FileReader();

            CaesarEncryptor caesarDecryptor = new CaesarEncryptor();

            if (format != FileFormat.etxt.ToValidFileFormat())
            {
                formattedTextFromSourceFile = await fileReader.ReadContentFromFileAsync(sourceFilePath);

                return formattedTextFromSourceFile;
            }
            else
            {
                string shiftArgument = args.FirstOrDefault(arg => arg.Contains(Argument.shift.ToValidArgument()));

                string directionArgument = args.FirstOrDefault(arg => arg.Contains(Argument.direction.ToValidArgument()));

                if (shiftArgument != null && directionArgument != null)
                {
                    int shift = caesarDecryptor.GetShiftFromCommandLine(shiftArgument);

                    string direction = caesarDecryptor.GetDirectionFromCommandLine(directionArgument);

                    string content = await File.ReadAllTextAsync(sourceFilePath);

                    if (direction == "left")
                    {
                        formattedTextFromSourceFile = caesarDecryptor.LeftShiftCipher(content, shift);

                        return formattedTextFromSourceFile;
                    }
                    else if (direction == "right")
                    {
                        formattedTextFromSourceFile = caesarDecryptor.RightShiftCipher(content, shift);

                        return formattedTextFromSourceFile;
                    }
                }
                else
                {
                    throw new ArgumentException("--direction or --shift argument is missing");
                }
            }

            throw new FormatException($"You have specified wrong file format {format}");
        }
    }
}