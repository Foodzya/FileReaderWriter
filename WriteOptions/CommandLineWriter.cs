using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileReaderWriter.TextManipulations;
using FileReaderWriter.Extensions;
using static FileReaderWriter.Enums.ArgumentEnum;
using System.Collections.Generic;
using static FileReaderWriter.Enums.FileFormatEnum;

namespace FileReaderWriter.WriteOptions
{
    public class CommandLineWriter
    {
        public async Task WriteFromCommandLine(string[] args)
        {
            if (Array.Exists(args, arg => arg == Argument.bulk.ToValidArgument()))
            {
                string targetDirectory = string.Empty;

                string fileFormat = string.Empty;

                FileInfo[] txtFiles = null;

                List<Argument> allowedArguments = Enum.GetValues(typeof(Argument)).Cast<Argument>().ToList();

                Task<FileInfo[]> txtFilesTask = null;

                Task<string> targetDirectoryTask = null;

                Task<string> fileFormatTask = null;

                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];

                    switch (argument)
                    {
                        case string sourceArg when sourceArg.Contains(Argument.source.ToValidArgument()):
                            txtFilesTask = Task.Factory.StartNew(() => GetTxtFilesFromSourcePath(sourceArg));
                            break;
                        case string targetArg when targetArg.Contains(Argument.target.ToValidArgument()):
                            targetDirectoryTask = Task.Factory.StartNew(() => GetTargetDirectory(targetArg));
                            break;
                        case string formatArg when formatArg.Contains(Argument.format.ToValidArgument()):
                            fileFormatTask = Task.Factory.StartNew(() => GetTargetFileFormat(formatArg));
                            break;
                        default:
                            if (!allowedArguments.Any(arg => argument.Contains(arg.ToValidArgument())))
                                throw new Exception("Unknown argument");
                            break;
                    }
                }

                Task[] allTasks = new Task[] { txtFilesTask, targetDirectoryTask, fileFormatTask };

                Task.WaitAll(allTasks);

                txtFiles = txtFilesTask.Result;
                targetDirectory = targetDirectoryTask.Result;
                fileFormat = fileFormatTask.Result;

                await WriteFromTxtFilesToNew(txtFiles, args, targetDirectory, fileFormat);
            }
            else
            {
                Console.WriteLine("To use multiple write option command line must contain --bulk argument");
            }
        }

        private async Task WriteFromTxtFilesToNew(
            FileInfo[] txtFiles,
            string[] args,
            string targetDirectory,
            string fileFormat)
        {
            FileWriter fileWriter = new FileWriter();
            TxtWriter txtWriter = new TxtWriter();

            if (fileFormat != FileFormat.etxt.ToValidFileFormat())
            {
                foreach (FileInfo txtFile in txtFiles)
                {
                    try
                    {
                        using (StreamReader sr = txtFile.OpenText())
                        {
                            string txtFileContent = sr.ReadToEnd();

                            string fileName = Path.GetFileNameWithoutExtension(txtFile.Name);

                            string targetFile = $@"{targetDirectory}\{fileName}{fileFormat}";

                            await fileWriter.WriteToFileAsync(txtFileContent, targetFile);
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {
                if (Array.Exists(args, arg => arg.Contains(Argument.shift.ToValidArgument())) && Array.Exists(args, arg => arg.Contains(Argument.direction.ToValidArgument())))
                {
                    string argument = string.Empty;

                    int shift = 0;

                    string direction = string.Empty;

                    CaesarEncryptor caesarEncryptor = new CaesarEncryptor();

                    Parallel.For(0, args.Length,
                                index =>
                                {
                                    argument = args[index];
                                    switch (argument)
                                    {
                                        case string encryptorShiftArg when encryptorShiftArg.Contains(Argument.shift.ToValidArgument()):
                                            shift = caesarEncryptor.GetShiftFromCommandLine(encryptorShiftArg);
                                            break;
                                        case string encryptorDirectionArg when encryptorDirectionArg.Contains(Argument.direction.ToValidArgument()):
                                            direction = caesarEncryptor.GetDirectionFromCommandLine(encryptorDirectionArg);
                                            break;
                                    }
                                });

                    foreach (FileInfo txtFile in txtFiles)
                    {
                        try
                        {
                            using (StreamReader sr = txtFile.OpenText())
                            {
                                string txtFileContent = await sr.ReadToEndAsync();

                                string fileName = Path.GetFileNameWithoutExtension(txtFile.Name);

                                string targetFile = $@"{targetDirectory}\{fileName}{fileFormat}";

                                string formattedContent = string.Empty;

                                if (direction == "left")
                                    formattedContent = caesarEncryptor.LeftShiftCipher(txtFileContent, shift);
                                else if (direction == "right")
                                    formattedContent = caesarEncryptor.RightShiftCipher(txtFileContent, shift);

                                await txtWriter.WriteToFileAsync(formattedContent, targetFile);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else
                {
                    throw new IOException("Command line must contain --shift=<encryptor_shift> and --direction=<encryptor_direction> arguments along with .etxt file format");
                }
            }
        }

        private FileInfo[] GetTxtFilesFromSourcePath(string sourceArgument)
        {
            int pathIndex = sourceArgument.IndexOf('=') + 1;

            string sourcePath = sourceArgument.Substring(pathIndex);

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);

                FileInfo[] txtFiles = directoryInfo.GetFiles("*.txt");

                return txtFiles;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        private string GetTargetDirectory(string targetArgument)
        {
            int pathIndex = targetArgument.IndexOf('=') + 1;

            string targetPath = targetArgument.Substring(pathIndex);

            if (Directory.Exists(targetPath))
            {
                return targetPath;
            }
            else
            {
                throw new IOException("Target directory doesn't exist");
            }
        }

        private string GetTargetFileFormat(string formatArgument)
        {
            int formatIndex = formatArgument.IndexOf('=') + 1;

            string format = formatArgument.Substring(formatIndex);

            return Path.GetExtension(format);
        }
    }
}