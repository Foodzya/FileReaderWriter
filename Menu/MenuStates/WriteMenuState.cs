using FileReaderWriter.WriteOptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReaderWriter.Menu.MenuStates
{
    public class WriteMenuState : MenuState
    {
        public override void DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine("WRITE MENU\n" +
                "—————————————————————————————\n" +
                "1 -- Write text (only .txt, .rtxt, .etxt, .btxt are applicable)\n" +
                "2 -- Back to the main menu");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    _menuContext.WriteToSpecificFile();
                    break;
                case ConsoleKey.D2:
                    _menuContext.PressBack();
                    break;
                default:
                    _menuContext.ChangeMenuState(new WriteMenuState());
                    break;
            }
        }

        public override void PressBack()
        {
            _menuContext.ChangeMenuState(new MainMenuState());
        }

        public override async Task ReadFromSpecificFileAsync()
        {
            await Task.Run(() => Console.WriteLine("That's doing nothing special"));
        }

        public override async Task WriteToSpecificFileAsync()
        {
            string content = await GetSourceText();

            string targetFile = GetPathToTargetFile();

            FileWriter fileWriter = new FileWriter();

            await fileWriter.WriteToFileAsync(content, targetFile);

            _menuContext.ChangeMenuState(new MainMenuState());
        }

        private async Task<string> GetSourceText()
        {
            Console.Clear();

            Console.WriteLine("READ OPTIONS\n" +
                "—————————————————————————————\n" +
                "1 -- To read text from txt\n" +
                "2 -- To read text from console input\n" +
                "3 -- Back to the menu\n");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    return await GetTextFromTxtFile();
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.Write("Your text: ");
                    return Console.ReadLine();
                case ConsoleKey.D3:
                    _menuContext.DisplayMenu();
                    break;
                default:
                    Console.WriteLine("An error occured.\n" +
                        "Press any button to try again..");
                    Console.ReadKey();
                    await GetSourceText();
                    break;
            }

            return string.Empty;
        }

        private async Task<string> GetTextFromTxtFile()
        {
            Console.WriteLine("Specify path to the txt file..\n"
                + @"Example: C:\ExampleFolder\example.txt\n"
                + "Type q to return to write menu");

            string path = Console.ReadLine();

            if (File.Exists(path))
            {
                return await File.ReadAllTextAsync(path);
            }
            else if (path.ToLower() == "q")
            {
                _menuContext.ChangeMenuState(new WriteMenuState());
            }

            Console.WriteLine("An error occured\n" +
                "Please check if specified txt file exists.\n" +
                "Press any button to try again.\n");

            Console.ReadKey();

            await GetTextFromTxtFile();

            return string.Empty;
        }

        private string GetPathToTargetFile()
        {
            Console.Clear();

            Console.WriteLine("Specify target file to write to (only .txt, .rtxt, .etxt, .btxt files are applicable..\n" +
                "Type Q to return back to menu");

            string path = Console.ReadLine();

            if (File.Exists(path))
            {
                return path;
            }
            else if (path.ToLower() == "q")
            {
                _menuContext.ChangeMenuState(new MainMenuState());
            }
            else
            {
                Console.WriteLine("An error occured\n" +
                    "1 -- Try again\n" +
                    "Press any button to return to the menu");

                if (Console.ReadKey().Key == ConsoleKey.D1)
                {
                    GetPathToTargetFile();
                }
                else
                {
                    _menuContext.ChangeMenuState(new WriteMenuState());
                }
            }

            return string.Empty;
        }
    }
}