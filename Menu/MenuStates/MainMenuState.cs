using System;

namespace FileReaderWriter.Menu.MenuStates
{
    public class MainMenuState : MenuState
    {
        public override void PressBack()
        {
            Environment.Exit(0);
        }

        public override void ReadFromSpecificFile()
        {
            Console.WriteLine("Nothing special...");
        }

        public override void WriteToSpecificFile()
        {
            Console.WriteLine("Nothing special...");
        }
        public override void DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine("MAIN MENU\n" +
                "—————————————————————————————\n" +
                "1 -- Read from a file with specific type to txt/console (only txt, rtxt, etxt, btxt)\n" +
                "2 -- Write from txt/console to a file with specific type (only txt)\n" +
                "3 -- Quit the application");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    _menuContext.ChangeMenuState(new ReadMenuState());
                    break;
                case ConsoleKey.D2:
                    _menuContext.ChangeMenuState(new WriteMenuState());
                    break;
                case ConsoleKey.D3:
                    _menuContext.PressBack();
                    break;
                default:
                    _menuContext.ChangeMenuState(new MainMenuState());
                    break;
            }
        }
    }
}