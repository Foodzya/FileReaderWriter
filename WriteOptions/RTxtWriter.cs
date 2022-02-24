using System;
using System.IO;

namespace FileReaderWriter.WriteOptions
{
    public class RtxtWriter : IWriteAction
    {
        public void WriteToFile(string content, string targetFile)
        {
            char[] charArray = content.ToCharArray();

            Array.Reverse(charArray);

            try
            {
                using (StreamWriter sw = File.CreateText(targetFile))
                {
                    sw.WriteLine(charArray);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message + "\nPlease specify rtxt file for writing there");
            }
        }
    }
}
