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
            }
        }

        public override void PressBack()
        {
            _menuContext.ChangeMenuState(new MainMenuState());
        }

        public override void ReadFromSpecificFile()
        {
            Console.Clear();

            Console.WriteLine("Specify path to the file to read from..\n" +
                @"Example: C:\ExampleFolder\example.txt");

            string? path = Console.ReadLine();

            FileReader fileReader = new FileReader();

            if (File.Exists(path))
            {
                string content = fileReader.ReadContentFromFile(path);

                FileWriter fileWriter = new FileWriter(new TxtWriter());

                Console.WriteLine("Specify destination txt file...");

                string? targetFile = Console.ReadLine();

                if (File.Exists(targetFile))
                {
                    fileWriter.WriteToFile(content, targetFile);
                }

                _menuContext.ChangeMenuState(new MainMenuState());
            }
            else
            { 
                _menuContext.ChangeMenuState(new ReadMenuState());
            }
        }

        public override void WriteToSpecificFile()
        {
            Console.WriteLine("That's doing nothing here");
        }
    }
}