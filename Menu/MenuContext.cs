using FileReaderWriter.Menu.MenuStates;

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
            _menuState.ReadFromSpecificFileAsync();
        }

        public void WriteToSpecificFile()
        {
            _menuState.WriteToSpecificFileAsync();
        }

        public void PressBack()
        {
            _menuState.PressBack();
        }

        public void DisplayMenu()
        {
            _menuState.DisplayMenu();
        }
    }
}