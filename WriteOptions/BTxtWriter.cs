using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileReaderWriter.WriteOptions
{
    public class BTxtWriter : IWriteAction
    {
        public void WriteToFile(string content, string targetFile)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            string result = string.Join(" ", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

            try
            {
                using (StreamWriter sw = File.CreateText(targetFile))
                {
                    sw.WriteLine(result);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message + "\nPlease specify btxt file for writing there");
            }
        }
    }
}
