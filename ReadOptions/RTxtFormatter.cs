using System;

namespace FileReaderWriter.ReadOptions
{
    public class RTxtFormatter : IReadFormatter
    {
        public string FormatContent(string content)
        {
            char[] charArray = content.ToCharArray();

            Array.Reverse(charArray);

            return new string(charArray);
        }
    }
}
