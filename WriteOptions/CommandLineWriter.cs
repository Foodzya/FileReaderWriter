using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileReaderWriter.TextManipulations;

namespace FileReaderWriter.WriteOptions
{
    public class CommandLineWriter
    {
        private List<string> allowedArguments = new List<string>
        {
            "--interactive",
            "--bulk",
            "--source=",
            "--target=",
            "--format=",
            "--shift=",
            "--direction="
        };

        public void WriteFromCommandLine(string[] args)
        {
            if (Array.Exists(args, arg => arg == "--bulk"))
            {
                string targetDirectory = string.Empty;

                string fileFormat = string.Empty;

                FileInfo[] txtFiles = null;

                Task<FileInfo[]> txtFilesTask = null;

                Task<string> targetDirectoryTask = null;

                Task<string> fileFormatTask = null;

                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];

                    switch (argument)
                    {
                        case string sourceArg when sourceArg.Contains("--source="):
                            txtFilesTask = Task.Factory.StartNew(() => GetTxtFilesFromSourcePath(sourceArg));
                            break;
                        case string targetArg when targetArg.Contains("--target="):
                            targetDirectoryTask = Task.Factory.StartNew(() => GetTargetDirectory(targetArg));
                            break;
                        case string formatArg when formatArg.Contains("--format="):
                            fileFormatTask = Task.Factory.StartNew(() => GetTargetFileFormat(formatArg, args));
                            break;
                        default:
                            if (!allowedArguments.Any(arg => argument.Contains(arg)))
                                throw new Exception("Unknown argument!");
                            break;
                    }
                }

                Task[] allTasks = new Task[] { txtFilesTask, targetDirectoryTask, fileFormatTask };

                Task.WaitAll(allTasks);

                txtFiles = txtFilesTask.Result;
                targetDirectory = targetDirectoryTask.Result;
                fileFormat = fileFormatTask.Result;

                Task writeFromTxtFilesTask = Task.Factory.StartNew(() => WriteFromTxtFilesToNew(txtFiles, args, targetDirectory, fileFormat));
                writeFromTxtFilesTask.Wait();
            }
            else
            {
                Console.WriteLine("To use command-line write feature first argument must be --bulk");
            }
        }

        private void WriteFromTxtFilesToNew(FileInfo[] txtFiles, string[] args, string targetDirectory, string fileFormat)
        {
            FileWriter fileWriter = new FileWriter();
            TxtWriter txtWriter = new TxtWriter();

            if (fileFormat == ".etxt")
            {
                if (Array.Exists(args, arg => arg.Contains("--shift=")) && Array.Exists(args, arg => arg.Contains("--direction=")))
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
                                        case string encryptorShiftArg when encryptorShiftArg.Contains("--shift="):
                                            shift = GetCaesarEncryptorShift(encryptorShiftArg);
                                            break;
                                        case string encryptorDirectionArg when encryptorDirectionArg.Contains("--direction="):
                                            direction = GetCaesarEncryptorDirection(encryptorDirectionArg);
                                            break;
                                    }
                                });

                    Parallel.ForEach(txtFiles, txtFile =>
                    {
                        try
                        {
                            using (StreamReader sr = txtFile.OpenText())
                            {
                                string txtFileContent = sr.ReadToEnd();

                                string fileName = Path.GetFileNameWithoutExtension(txtFile.Name);

                                string targetFile = $@"{targetDirectory}\{fileName}{fileFormat}";

                                string formattedContent = string.Empty;

                                if (direction == "left")
                                    formattedContent = caesarEncryptor.LeftShiftCipher(txtFileContent, shift);
                                else if (direction == "right")
                                    formattedContent = caesarEncryptor.RightShiftCipher(txtFileContent, shift);

                                txtWriter.WriteToFile(formattedContent, targetFile);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    });
                }
                else
                {
                    throw new IOException("Command line must contain --shift=<encryptor_shift> and --direction=<encryptor_direction> arguments along with .etxt file format");
                }
            }
            else
            {
                Parallel.ForEach(txtFiles, txtFile => 
                {
                    try
                    {
                        using (StreamReader sr = txtFile.OpenText())
                        {
                            string txtFileContent = sr.ReadToEnd();

                            string fileName = Path.GetFileNameWithoutExtension(txtFile.Name);

                            string targetFile = $@"{targetDirectory}\{fileName}{fileFormat}";

                            fileWriter.WriteToFile(txtFileContent, targetFile);
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
        }

        private string GetCaesarEncryptorDirection(string encryptorDirectionArgument)
        {
            int directionIndex = encryptorDirectionArgument.IndexOf('=') + 1;

            string direction = encryptorDirectionArgument.Substring(directionIndex);

            if (direction == "right" || direction == "left")
            {
                return direction;
            }
            else
            {
                throw new ArgumentException("--direction argument can have only 'right' or 'left' values");
            }
        }

        private int GetCaesarEncryptorShift(string encryptorShiftArgument)
        {
            int shiftIndex = encryptorShiftArgument.IndexOf('=') + 1;

            int shift = 0;

            string shiftAsString = encryptorShiftArgument.Substring(shiftIndex);

            try
            {
                shift = int.Parse(shiftAsString);
            }
            catch (FormatException exception)
            {
                Console.WriteLine("Incorrect shift argument\n" + exception);
            }

            return shift;
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

        private string GetTargetFileFormat(string formatArgument, string[] args)
        {
            int formatIndex = formatArgument.IndexOf('=') + 1;

            string format = formatArgument.Substring(formatIndex);

            return Path.GetExtension(format);
        }
    }
}