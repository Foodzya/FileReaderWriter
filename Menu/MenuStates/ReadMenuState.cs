using System;
using System.IO;
using FileReaderWriter.ReadOptions;
using FileReaderWriter.WriteOptions;

namespace FileReaderWriter.Menu.MenuStates
{
    public class ReadMenuState : MenuState
    {
        public override void DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine("READ MENU\n" +
                "—————————————————————————————\n" +
                "1 -- Read from (only txt, rtxt, etxt, btxt)\n" +
                "2 -- Back to the main menu");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    _menuContext.ReadFromSpecificFile();
                    break;
                case ConsoleKey.D2:
                    _menuContext.PressBack();
                    break;
                default:
                    _menuContext.ChangeMenuState(new ReadMenuState());
                    break;

            }
        }

        public override void PressBack()
        {
            _menuContext.ChangeMenuState(new MainMenuState());
        }

        public override void ReadFromSpecificFile()
        {
            Console.Clear();

            Console.WriteLine("Specify path to the file to read from (only .txt, .rtxt, .etxt, .btxt formats are applicable)..\n" +
                @"Example: C:\ExampleFolder\example.txt");

            string? path = Console.ReadLine();

            FileReader fileReader = new FileReader();

            if (File.Exists(path))
            {
                string content = fileReader.ReadContentFromFile(path);

                WriteTo(content);

                _menuContext.ChangeMenuState(new MainMenuState());
            }
            else
            { 
                _menuContext.ChangeMenuState(new ReadMenuState());
            }
        }

        private void WriteTo(string content)
        {
            Console.WriteLine("\nWhere would you like to write text?\n" +
            "1 -- Txt file\n" +
            "2 -- Console\n" + 
            "3 -- Back to the menu\n");

            string targetFile = string.Empty;
            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Specify destination txt file..\n" +
                    "Enter Q to return to the menu..");
                    targetFile = Console.ReadLine();
                    if (targetFile.ToLower() == "q")
                        _menuContext.ChangeMenuState(new ReadMenuState());
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine($"\nHere is the final result: {content}\n" +
                    "Press anything to continue..");
                    Console.ReadKey();
                    break;
                case ConsoleKey.D3:
                    _menuContext.ChangeMenuState(new ReadMenuState());
                    break;
                default:
                    Console.WriteLine("\nTry again...\nPress any button to continue..");
                    Console.ReadKey();
                    break;
            }

                if (File.Exists(targetFile))
                {
                    TxtWriter txtWriter = new TxtWriter();
                    txtWriter.WriteToFile(content, targetFile);
                }
                else 
                {
                    _menuContext.ChangeMenuState(new ReadMenuState());
                }
                
                Console.WriteLine("Press any button to continue...");
                Console.ReadKey();
        }

        public override void WriteToSpecificFile()
        {
            Console.WriteLine("That's doing nothing here");
        }
    }
}