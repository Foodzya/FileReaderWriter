using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaderWriter.ReadOptions
{
    public class BtxtReader : IFileReader
    {
        public string FormatContent(string content)
        {
            string formattedContent;

            try 
            {
                formattedContent = GetStringFromBytes(content); 

                return formattedContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return string.Empty;
        }

        private string GetStringFromBytes(string content) 
        {
            List<byte> byteList = new List<byte>();

            byte convertedItem;

            for (int i = 0; i < content.Length; i += 8)
            {
                convertedItem = Convert.ToByte(content.Substring(i, 8), 2);

                byteList.Add(convertedItem);
            }

            string convertedContent = Encoding.UTF8.GetString(byteList.ToArray());

            return convertedContent;
        }
    }
}
