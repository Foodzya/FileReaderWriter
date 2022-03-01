using System;
using System.IO;
using FileReaderWriter.WriteOptions;

namespace FileReaderWriter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Launcher launcher = new Launcher();

                launcher.LaunchMenu();
            }
            else if (args.Length == 4)
            {
                CommandLineWriter cmWriter = new CommandLineWriter();

                cmWriter.WriteFromCommandLine(args); 
            }
            else 
            {
                Console.WriteLine("Not enough arguments for write operation\n" +
                    "Check for required arguments:\n" +
                    "--bulk (must be the first argument)\n" +
                    "--source=<dir_name>\n" +
                    "--target=<dir_name>\n" +
                    "--format=<format_type>");
            }
        }
    }
}