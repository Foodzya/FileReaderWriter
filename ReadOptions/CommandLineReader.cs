using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileReaderWriter.Extensions;
using FileReaderWriter.TextManipulations;
using static FileReaderWriter.Enums.ArgumentEnum;
using static FileReaderWriter.Enums.FileFormatEnum;

namespace FileReaderWriter.ReadOptions
{
    public class CommandLineReader
    {
        public static async Task<string> GetTextFromSourceFileAsync(string sourceFilePath, string[] args)
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