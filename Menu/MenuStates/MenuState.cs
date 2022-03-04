using System.Threading.Tasks;

namespace FileReaderWriter.Menu.MenuStates
{
    abstract public class MenuState
    {
        protected MenuContext _menuContext;
        public abstract Task ReadFromSpecificFileAsync();
        public abstract Task WriteToSpecificFileAsync();
        public abstract void PressBack();
        public abstract void DisplayMenu();
        public void SetMenuContext(MenuContext menuContext)
        {
            _menuContext = menuContext;
        }
    }
}