using System;
using System.IO;

namespace FileReaderWriter.WriteOptions
{
    public class TxtWriter : IFileWriter
    {
        public void WriteToFile(string content, string targetFile)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(targetFile))
                {
                    sw.WriteLine(content);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message + "\nPlease specify txt file for writing there");
            }
        }
    }
}
