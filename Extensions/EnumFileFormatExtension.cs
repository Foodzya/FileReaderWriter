using static FileReaderWriter.Enums.FileFormatEnum;

namespace FileReaderWriter.Extensions
{
    public static class EnumFileFormatExtensions
    {
        public static string ToValidFileFormat(this FileFormat arg)
        {
            return string.Concat(".", arg);
        }
    }
}