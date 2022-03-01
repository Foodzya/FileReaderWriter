using System;
using System.IO;

namespace FileReaderWriter.WriteOptions
{
    public class CommandLineWriter
    {
        public void WriteFromCommandLine(string[] args)
        {
            if (args[0] == "--bulk")
            {
                string targetPath = string.Empty;

                string fileFormat = string.Empty;

                FileInfo[] txtFiles;

                for (int i = 1; i < args.Length; i++)
                {
                    string command = args[i];

                    switch (command)
                    {
                        case string sourceArg when sourceArg.Contains("--source="):
                            txtFiles = GetTxtFilesFromSourcePath(sourceArg);
                            break;
                        case string targetArg when targetArg.Contains("--target="):
                            targetPath = GetTargetDirectory(targetArg);
                            break;
                        case string formatArg when formatArg.Contains("--format="):
                            fileFormat = GetTargetFileFormat(formatArg);
                            break;
                        default:
                            Console.WriteLine("Command-line contains unknown argument(-s)");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("To use command-line write feature first argument must be --bulk");
            }
        }

        private void WriteFromTxtFilesToNew(FileInfo[] txtFiles, string targetPath, string fileFormat)
        {
            foreach (FileInfo txtFile in txtFiles)
            {

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

            return null;
        }

        private string GetTargetFileFormat(string formatArgument)
        {
            int formatIndex = formatArgument.IndexOf('=') + 1;

            string format = formatArgument.Substring(formatIndex);

            return Path.GetExtension(format);
        }
    }

}