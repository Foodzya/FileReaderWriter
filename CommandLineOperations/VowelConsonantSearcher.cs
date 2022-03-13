using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileReaderWriter.Extensions;
using FileReaderWriter.ReadOptions;
using static FileReaderWriter.Enums.ArgumentEnum;

namespace FileReaderWriter.CommandLineOperations
{
    public class VowelConsonantSearcher
    {
        readonly char[] vowels = { 'a', 'e', 'i', 'y', 'o', 'u' };
        public async Task PrintNumberOfVowelsConsonants(string[] args)
        {            
            var allowedArguments = new List<Argument> { Argument.source, Argument.vowels, Argument.shift, Argument.direction };
            string sourceText = null;
            string sourceFilePath = null;

            for (var i = 0; i < args.Length; i++)
            {
                string argument = args[i];

                switch (argument)
                {
                    case string sourceArg when sourceArg.Contains(Argument.source.ToValidArgument()):
                        sourceFilePath = PathReader.GetPathFromCommandLineArgument(sourceArg);
                        sourceText = await CommandLineReader.GetTextFromSourceFileAsync(sourceFilePath, args);
                        break;
                    default:
                        if (!allowedArguments.Any(arg => argument.Contains(arg.ToValidArgument())))
                            throw new ArgumentException($"Wrong {argument} argument");
                        break;
                }
            }

            int vowelCount = sourceText
                .ToLower()
                .Where(ch => vowels.Contains(ch))
                .Count();

            int consonantCount = sourceText
                .ToLower()
                .Where(ch => char.IsLetter(ch) && !vowels.Contains(ch))
                .Count();

            Console.WriteLine($"Vowels: {vowelCount}, consonants: {consonantCount}");
        }
    }
}