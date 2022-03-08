using System;
using System.Threading.Tasks;
using FileReaderWriter.Extensions;
using FileReaderWriter.WriteOptions;
using static FileReaderWriter.Enums.ArgumentEnum;

namespace FileReaderWriter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            int minRequiredArgs = 4;

            if (args[0] == Argument.interactive.ToValidArgument() || args.Length == 0)
            {
                Launcher launcher = new Launcher();

                launcher.LaunchMenu();
            }
            else if (args.Length >= minRequiredArgs)
            {
                CommandLineWriter cmWriter = new CommandLineWriter();

                await cmWriter.WriteFromCommandLine(args);
            }
            else
            {
                Console.WriteLine("Not enough arguments for write operation\n" +
                    "Check for required arguments:\n" +
                    "--interactive (must be the first argument)\n" +
                    "--bulk (for massive write process)\n" +
                    "--source=<dir_name>\n" +
                    "--target=<dir_name>\n" +
                    "--format=<format_type>\n\n" +
                    "Additional arguments (for .etxt format):\n" +
                    "--shift=<encryptor_shift>\n" +
                    "--direction=<encryptor_direction>");
            }
        }
    }
}