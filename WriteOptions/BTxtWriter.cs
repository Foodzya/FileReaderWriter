using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileReaderWriter.WriteOptions
{
    public class BtxtWriter : IFileWriter
    {
        public void WriteToFile(string content, string targetFile)
        {
            byte[] fileContentInBytes = Encoding.UTF8.GetBytes(content);

            string binaryResult = string.Join("", fileContentInBytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

            try
            {
                using (StreamWriter sw = File.CreateText(targetFile))
                {
                    sw.WriteLine(binaryResult);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message + "\nPlease specify btxt file for writing there");
            }
        }
    }
}