using static FileReaderWriter.Enums.ArgumentEnum;

namespace FileReaderWriter.Extensions
{
    public static class EnumArgumentExtensions
    {
        public static string ToValidArgument(this Argument arg)
        {
            if (arg == Argument.interactive || arg == Argument.bulk)
            {
                return "--" + arg;
            }
            else
            {
                return "--" + arg + "=";
            }
        }
    }
}
