using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaderWriter.ReadOptions
{
    public class BTxtFormatter : IReadFormatter
    {
        public string FormatContent(string content)
        {
            List<byte> byteList = new List<byte>();

            for(int i = 8; i < content.Length; i+=8)
            {
                byteList.Add(Convert.ToByte(content.Substring(i, 8), 2));
            }

            return Encoding.UTF8.GetString(byteList.ToArray());
        }
    }
}
