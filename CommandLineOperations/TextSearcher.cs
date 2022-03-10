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
            string searchText = null;

            List<Argument> allowedArguments = Enum.GetValues(typeof(Argument)).Cast<Argument>().ToList();

            foreach (var arg in args)
            {
                if (arg.Contains(Argument.search.ToValidArgument()))
                {
                    int pathIndex = arg.IndexOf('=') + 1;

                    searchText = await GetTextFromSourceFile(arg.Substring(pathIndex), args);
                    
                }
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


    }
}