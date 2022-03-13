using System.IO;

namespace FileReaderWriter.CommandLineOperations
{
    public static class PathReader
    {
        public static string GetPathFromCommandLineArgument(string argument)
        {
            int pathIndex = argument.IndexOf('=') + 1;

            string path = argument.Substring(pathIndex).Trim();

            if (File.Exists(path))
            {
                return path;
            }

            throw new DirectoryNotFoundException($"Directory {path} doesn't exist");
        }
    }
}