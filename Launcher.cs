using FileReaderWriter.Menu;
using System;

namespace FileReaderWriter
{
    public class Launcher
    {
        public void LaunchMenu()
        {
            MenuContext menu = new MenuContext();

            menu.DisplayMenu();

            Console.ReadLine();
        }
    }
}