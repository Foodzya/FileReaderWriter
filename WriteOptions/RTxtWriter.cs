using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReaderWriter.WriteOptions
{
    public class RtxtWriter : IFileWriter
    {
        public async Task WriteToFileAsync(string content, string targetFile)
        {
            char[] charArray = content.ToCharArray();

            Array.Reverse(charArray);

            try
            {
                using (StreamWriter sw = File.CreateText(targetFile))
                {
                    await sw.WriteLineAsync(charArray);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message + "\nPlease specify rtxt file for writing there");
            }
        }
    }
}
