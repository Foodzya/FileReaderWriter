using System;
using System.IO;

namespace FileReaderWriter.WriteOptions
{
    public class FileWriter
    {
        private IFileWriter _writer;

        public FileWriter()
        {

        }

        public void WriteToFile(string content, string targetFile)
        {
            ValidateFileExtension(targetFile);

            _writer.WriteToFile(content, targetFile);
        }

        private void SetWriter(IFileWriter writer)
        {
            _writer = writer;
        }

        private void ValidateFileExtension(string path)
        {
            string extension = Path.GetExtension(path);

            switch (extension)
            {
                case ".txt":
                    SetWriter(new TxtWriter());
                    break;
                case ".rtxt":
                    SetWriter(new RtxtWriter());
                    break;
                case ".etxt":
                    SetWriter(new EtxtWriter());
                    break;
                case ".btxt":
                    SetWriter(new BtxtWriter());
                    break;
                default:
                    Console.WriteLine("This file type is unsupported.\n" +
                        "Press anything to continue..");
                    break;
            }
        }
    }
}