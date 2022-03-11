using System;
using System.Collections.Generic;
using System.Linq;
using FileReaderWriter.Extensions;
using FileReaderWriter.ReadOptions;
using static FileReaderWriter.Enums.ArgumentEnum;

namespace FileReaderWriter.CommandLineOperations
{
    public class VowelConsonantSearcher
    {
        public async void PrintNumberOfVowelsConsonants(string[] args)
        {
            List<Argument> allowedArguments = new List<Argument> { Argument.source, Argument.vowels, Argument.shift, Argument.direction };

            string sourceText = null;

            for (var i = 0; i < args.Length; i++)
            {
                string argument = args[i];

                switch (argument)
                {
                    case string sourceArg when sourceArg.Contains(Argument.source.ToValidArgument()):
                        int sourcePathIndex = sourceArg.IndexOf('=') + 1;
                        string sourceFilePath = sourceArg.Substring(sourcePathIndex);
                        sourceText = await CommandLineReader.GetTextFromSourceFileAsync(sourceFilePath, args);
                        break;
                    default:
                        if (!allowedArguments.Any(arg => argument.Contains(arg.ToValidArgument())))
                            throw new ArgumentException($"Wrong {argument} argument");
                        break;
                }
            }

            char[] vowels = new char[] { 'a', 'e', 'i', 'y', 'o', 'u' };

            int vowelCount = sourceText.ToLower().Where(ch => vowels.Contains(ch)).Count();

            int consonantCount = sourceText.ToLower().Where(ch => Char.IsLetter(ch) && !vowels.Contains(ch)).Count();

            Console.WriteLine($"Vowels: {vowelCount}, consonants: {consonantCount}");
        }
    }
}