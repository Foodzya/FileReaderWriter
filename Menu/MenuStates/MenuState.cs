namespace FileReaderWriter.Menu.MenuStates
{
    abstract public class MenuState
    {
        protected MenuContext _menuContext;
        public abstract void ReadFromSpecificFile();
        public abstract void WriteToSpecificFile();
        public abstract void PressBack();
        public abstract void DisplayMenu();
        public void SetMenuContext(MenuContext menuContext)
        {
            _menuContext = menuContext;
        }
    }
}