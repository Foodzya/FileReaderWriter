using System;
using System.IO;
using System.Threading.Tasks;
using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;

namespace FileReaderWriter.ReadOptions
{
    public class FileReader
    {
        private IFileReader _reader;

        private void SetReader(IFileReader reader)
        {
            _reader = reader;
        }

        public void SetReaderByFileFormat(string path)
        {
            string extension = Path.GetExtension(path);
            
            switch (extension)
            {
                case ".txt":
                    SetReader(new TxtReader());
                    break;
                case ".rtxt":
                    SetReader(new RtxtReader());
                    break;
                case ".etxt":
                    SetReader(new EtxtReader());
                    break;
                case ".btxt":
                    SetReader(new BtxtReader());
                    break;
                default:
                    Console.WriteLine("This file type is unsupported.\n" +
                        "Press any button to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        public async Task<string> ReadContentFromFileAsync(string path)
        {
            SetReaderByFileFormat(path);

            string content = await File.ReadAllTextAsync(path);

            return FormatContent(content);
        }

        private string FormatContent(string content)
        {
            if (_reader != null)
            {
                return _reader.FormatContent(content);
            }
            else
            {
                Console.WriteLine("\nAn error occured during reading the file. \nPlease check out the specified file extension!\nPress any button to continue..");

                Console.ReadKey();

                MenuContext menuContext = new MenuContext();

                menuContext.ChangeMenuState(new MainMenuState());
            }

            return string.Empty;
        }
    }
}
