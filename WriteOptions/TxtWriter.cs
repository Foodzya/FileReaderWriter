using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReaderWriter.WriteOptions
{
    public class TxtWriter : IFileWriter
    {
        public async Task WriteToFileAsync(string content, string targetFile)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(targetFile))
                {
                    await sw.WriteLineAsync(content);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message + "\nPlease specify txt file for writing there");
            }
        }
    }
}