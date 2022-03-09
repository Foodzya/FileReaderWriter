using FileReaderWriter.Extensions;
using FileReaderWriter.ReadOptions;
using FileReaderWriter.TextManipulations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static FileReaderWriter.Enums.ArgumentEnum;
using static FileReaderWriter.Enums.FileFormatEnum;

namespace FileReaderWriter.CommandLineOperations
{
    public class RepetitionCounter
    {
        public async void WriteWordsToJsonInDescending(string[] args)
        {
            Task<string> sourceFilePathTask = null;

            Task<string> targetJsonFileTask = null;

            List<FileFormat> allowedFileFormats = Enum.GetValues(typeof(FileFormat)).Cast<FileFormat>().ToList();

            string sourceFilePath;
            
            string textFromSourceFile;

            for (var i = 0; i < args.Length; i++)
            {
                string argument = args[i];

                switch (argument)
                {
                    case string sourceArg when sourceArg.Contains(Argument.source.ToValidArgument()):
                        sourceFilePathTask = Task.Factory.StartNew(() => GetSourceFilePath(sourceArg));
                        break;
                    case string targetArg when targetArg.Contains(Argument.target.ToValidArgument()):
                        targetJsonFileTask = Task.Factory.StartNew(() => GetTargetJsonFile(targetArg));
                        break;
                    case string repetitionArg when repetitionArg.Contains(Argument.repetitions.ToValidArgument()):
                        break;
                    default:
                        if (!allowedFileFormats.Any(format => argument.Contains(format.ToValidFileFormat())))
                            throw new ArgumentException($"Wrong {argument} argument");
                        break;
                }
            }

            Task[] allTasks = new Task[] { sourceFilePathTask, targetJsonFileTask };

            Task.WaitAll(allTasks);

            sourceFilePath = sourceFilePathTask.Result;

            textFromSourceFile = await GetTextFromSourceFile(sourceFilePath, args);

            SortWordsInDescendingToJson(textFromSourceFile);
        }

        private string SortWordsInDescendingToJson(string textFromSourceFile)
        {
            char[] delimeterChars = { ' ', ',', '.', ':', ';', '\t', '—', '-' };

            Dictionary<string, int> dictionaryWithRepeatedWords = new Dictionary<string, int>();

            List<string> listOfAllWords = textFromSourceFile.ToLower().Split(delimeterChars).ToList();

            listOfAllWords.ForEach(word =>
            {
                if (dictionaryWithRepeatedWords.ContainsKey(word))
                {
                    dictionaryWithRepeatedWords[word]++;
                }
                else
                {
                    dictionaryWithRepeatedWords.Add(word, 1);
                }
            });

            DictionaryToJson(dictionaryWithRepeatedWords);

            return null;
        }

        private void DictionaryToJson(Dictionary<string, int> dictionaryWithRepeatedWords)
        {
            var json = dictionaryWithRepeatedWords.Select(d => string.Format("\"{0}\": [{1}]", d.Key, d.Value));

            foreach(var j in json)
            {
                Console.WriteLine(j);
            }
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

            throw new FormatException($"You have specified wrong file format {format}");
        }

        private string GetTargetJsonFile(string targetArg)
        {
            string format = Path.GetExtension(targetArg);

            if (format == FileFormat.json.ToValidFileFormat())
            {
                return format;
            }

            throw new FormatException($"Only json format is valid. Wrong {format} format");
        }

        private string GetSourceFilePath(string sourceArg)
        {
            int pathIndex = sourceArg.IndexOf('=') + 1;

            string path = sourceArg.Substring(pathIndex).Trim();

            if (File.Exists(path))
            {
                return path;
            }

            throw new DirectoryNotFoundException($"Directory {path} doesn't exist");
        }
    }
}