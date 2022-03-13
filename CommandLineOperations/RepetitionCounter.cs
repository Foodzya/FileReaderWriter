using FileReaderWriter.Extensions;
using FileReaderWriter.ReadOptions;
using FileReaderWriter.WriteOptions;
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
        public async Task WriteWordsToJsonInDescendingAsync(string[] args)
        {
            Task<string> sourceFilePathTask = null;
            Task<string> targetJsonFileTask = null;
            Task[] allTasks;
            string sourceFilePath;
            string targetJsonPath = null;
            string textFromSourceFile;
            var allowedArguments = new List<Argument> { Argument.source, Argument.target, Argument.json, Argument.console, Argument.repetitions, Argument.shift, Argument.direction };

            for (var i = 0; i < args.Length; i++)
            {
                string argument = args[i];

                switch (argument)
                {
                    case string sourceArg when sourceArg.Contains(Argument.source.ToValidArgument()):
                        sourceFilePathTask = Task.Factory.StartNew(() => PathReader.GetPathFromCommandLineArgument(sourceArg));
                        break;
                    case string targetArg when targetArg.Contains(Argument.target.ToValidArgument()):
                        targetJsonFileTask = Task.Factory.StartNew(() => GetTargetJsonFile(targetArg));
                        break;
                    case string repetitionArg when repetitionArg.Contains(Argument.repetitions.ToValidArgument()):
                        break;
                    default:
                        if (!allowedArguments.Any(arg => argument.Contains(arg.ToValidArgument())))
                            throw new ArgumentException($"Wrong {argument} argument");
                        break;
                }
            }

            if (targetJsonFileTask != null)
            {
                allTasks = new Task[] { sourceFilePathTask, targetJsonFileTask };

                Task.WaitAll(allTasks);

                targetJsonPath = targetJsonFileTask.Result;
            }
            else
            {
                sourceFilePathTask.Wait();
            }

            sourceFilePath = sourceFilePathTask.Result;

            textFromSourceFile = await CommandLineReader.GetTextFromSourceFileAsync(sourceFilePath, args);

            Dictionary<string, int> keyValueOfRepetitiveWords = GetDictionaryOfRepetativeWords(textFromSourceFile);

            WriteDictionaryToJsonFileInDescending(keyValueOfRepetitiveWords, targetJsonPath, args);
        }

        private Dictionary<string, int> GetDictionaryOfRepetativeWords(string textFromSourceFile)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', ';', '!', '?', '\t', '\r', '\n', '—', '-', '"' };
            Dictionary<string, int> dictionaryWithRepetitiveWords = new Dictionary<string, int>();

            List<string> listOfAllWords = textFromSourceFile
                .ToLower()
                .Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            listOfAllWords.ForEach(word =>
            {
                if (dictionaryWithRepetitiveWords.ContainsKey(word))
                {
                    dictionaryWithRepetitiveWords[word]++;
                }
                else
                {
                    dictionaryWithRepetitiveWords.Add(word, 1);
                }
            });

            return dictionaryWithRepetitiveWords;
        }

        private void WriteDictionaryToJsonFileInDescending(Dictionary<string, int> dictionaryWithRepetitiveWords, string targetJsonPath, string[] args)
        {
            TxtWriter txtWriter = new TxtWriter();
            List<string> orderedTextInJsonFormat = dictionaryWithRepetitiveWords
                .OrderByDescending(word => word.Value)
                .Select(keyValuePair => string.Format("{0}:{1}", keyValuePair.Key, keyValuePair.Value))
                .ToList();

            if (args.Contains(Argument.json.ToValidArgument()) && targetJsonPath != null)
            {
                using (TextWriter tw = new StreamWriter(targetJsonPath))
                {
                    orderedTextInJsonFormat.ForEach(async str =>
                    {
                        if (str == orderedTextInJsonFormat.First())
                        {
                            await tw.WriteAsync("[ {" + str + "}, ");
                        }
                        else if (str == orderedTextInJsonFormat.Last())
                        {
                            await tw.WriteAsync("{" + str + "} ]");
                        }
                        else
                        {
                            await tw.WriteAsync("{" + str + "}, ");
                        }
                    });
                }
            }
            else if (args.Contains(Argument.console.ToValidArgument()))
            {
                orderedTextInJsonFormat.ForEach(str =>
                {
                    if (str == orderedTextInJsonFormat.First())
                    {
                        Console.Write("[ {" + str + "}, ");
                    }
                    else if (str == orderedTextInJsonFormat.Last())
                    {
                        Console.Write("{" + str + "} ]");

                    }
                    else
                    {
                        Console.Write("{" + str + "}, ");
                    }
                });
            }
            else
            {
                throw new ArgumentException("Command line must contain --json (along with --target= argument) or --console (--target argument not necessary)");
            }
        }

        private string GetTargetJsonFile(string targetArg)
        {
            string targetJsonPath = PathReader.GetPathFromCommandLineArgument(targetArg);

            string format = Path.GetExtension(targetJsonPath);

            if (format == FileFormat.json.ToValidFileFormat() && File.Exists(targetJsonPath))
            {
                return targetJsonPath;
            }

            throw new FormatException($"Only json format is valid. Wrong {format} format");
        }
    }
}