using System;
using System.IO;

namespace FileReaderWriter.ReadOptions
{
    public class FileReader
    {
        private IReadFormatter _formatter;

        public FileReader()
        {

        }

        public FileReader(IReadFormatter formatter)
        {
            _formatter = formatter;
        }

        private void SetFormatter(IReadFormatter formatter)
        {
            _formatter = formatter;
        }

        public void ValidateFileExtension(string path)
        {
            string extension = Path.GetExtension(path);

            switch (extension)
            {
                case ".txt":
                    SetFormatter(new TxtFormatter());
                    break;
                case ".rtxt":
                    SetFormatter(new RTxtFormatter());
                    break;
                case ".etxt":
                    SetFormatter(new ETxtFormatter());
                    break;
                case ".btxt":
                    SetFormatter(new BTxtFormatter());
                    break;
                default:
                    Console.WriteLine("This file type is unsupported.");
                    break;
            }
        }

        public string ReadContentFromFile(string path)
        {
            ValidateFileExtension(path);

            string content = File.ReadAllText(path);

            return FormatContent(content);
        }

        private string FormatContent(string content)
        {
            return _formatter.FormatContent(content);
        }
    }
}
