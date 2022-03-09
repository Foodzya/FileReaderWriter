using static FileReaderWriter.Enums.ArgumentEnum;

namespace FileReaderWriter.Extensions
{
    public static class EnumArgumentExtensions
    {
        public static string ToValidArgument(this Argument arg)
        {
            if (arg == Argument.interactive || arg == Argument.bulk || arg == Argument.repetitions)
            {
                return string.Concat("--", arg);
            }
            else
            {
                return string.Concat("--", arg, "=");
            }
        }
    }
}