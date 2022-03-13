using System;
using System.Threading.Tasks;
using FileReaderWriter.CommandLineOperations;
using FileReaderWriter.Extensions;
using FileReaderWriter.WriteOptions;
using static FileReaderWriter.Enums.ArgumentEnum;

namespace FileReaderWriter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const int argsForBulkWriter = 4;
            const int argsForRepetitionCounter = 3;
            const int argsForVowelsCounter = 2;
            const int argsForSearchCounter = 2;

            if (args.Length == 0 || args[0] == Argument.interactive.ToValidArgument())
            {
                Launcher launcher = new Launcher();

                launcher.LaunchMenu();
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];

                    switch (argument)
                    {
                        case string bulkArg when bulkArg.Contains(Argument.bulk.ToValidArgument()):
                            if (args.Length >= argsForBulkWriter)
                            {
                                CommandLineWriter cmWriter = new CommandLineWriter();

                                await cmWriter.WriteFromCommandLine(args);
                            }
                            else
                            {
                                Console.WriteLine("Not enough arguments for write operation\n" +
                                "Check for required arguments:\n" +
                                "--bulk (for multiple files write process)\n" +
                                "--source=<dir_name>\n" +
                                "--target=<dir_name>\n" +
                                "--format=<format_type>\n\n" +
                                "Additional arguments (for .etxt format):\n" +
                                "--shift=<encryptor_shift>\n" +
                                "--direction=<encryptor_direction>");
                            }
                            break;
                        case string repetitionsArg when repetitionsArg.Contains(Argument.repetitions.ToValidArgument()):
                            if (args.Length >= argsForRepetitionCounter)
                            {
                                RepetitionCounter repetitionCounter = new RepetitionCounter();

                                await repetitionCounter.WriteWordsToJsonInDescendingAsync(args);
                            }
                            else
                            {
                                throw new ArgumentException("Command line must contain --repetitions, --json (along with --target=<target_json_file>) or --console, --source=<source_file>\n" +
                                "Optional arguments if source file is .etxt: --shift=, --direction=");
                            }
                            break;
                        case string searchArg when searchArg.Contains(Argument.search.ToValidArgument()):
                            if (args.Length >= argsForSearchCounter)
                            {
                                TextSearcher textSearcher = new TextSearcher();

                                await textSearcher.SearchTextInFile(args);
                            }
                            else
                            {
                                throw new ArgumentException("Command line must contain --search=<search_file>, --source=<source_file>" +
                                "Optional arguments if source file is .etxt: --shift=, --direction=");
                            }
                            break;
                        case string vowelsArg when vowelsArg.Contains(Argument.vowels.ToValidArgument()):
                            if (args.Length >= argsForVowelsCounter)
                            {
                                VowelConsonantSearcher vowelConsonantSearcher = new VowelConsonantSearcher();

                                await vowelConsonantSearcher.PrintNumberOfVowelsConsonants(args);
                            }
                            else 
                            {
                                throw new ArgumentException("Command line must contain --vowels, --source=<source_file>\n" +
                                "Optional are --right and --direction if source file is .etxt");
                            }
                            break;
                    }
                }
            }
        }
    }
}