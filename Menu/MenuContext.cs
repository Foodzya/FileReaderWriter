using FileReaderWriter.Menu.MenuStates;
using System;

namespace FileReaderWriter.Menu
{
    public class MenuContext
    {
        private MenuState _menuState;

        public MenuContext()
        {
            _menuState = new MainMenuState();
            _menuState.SetMenuContext(this);
        }

        public void ChangeMenuState(MenuState menuState)
        {
            _menuState = menuState;

            _menuState.SetMenuContext(this);

            _menuState.DisplayMenu();
        }

        public void ReadFromSpecificFile()
        {
            _menuState.ReadFromSpecificFile();
        }

        public void WriteToSpecificFile()
        {
            ChangeMenuState(new WriteMenuState());

            _menuState.WriteToSpecificFile();
        }

        public void PressBack()
        {
            if (!(_menuState is MainMenuState))
            {
                _menuState.PressBack();
            }
            else
            {
                Console.WriteLine("Do you wanna quit the app?");
            }
        }

        public void DisplayMenu()
        {
            _menuState.DisplayMenu();
        }
    }
}