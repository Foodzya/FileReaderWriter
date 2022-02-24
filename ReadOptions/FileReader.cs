using System;
using System.IO;
using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;

namespace FileReaderWriter.ReadOptions
{
    public class FileReader
    {
        private IFileReader _reader;

        public FileReader()
        {

        }

        public FileReader(IFileReader reader)
        {
            _reader = reader;
        }

        private void SetReader(IFileReader reader)
        {
            _reader = reader;
        }

        public void ValidateFileExtension(string path)
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

        public string ReadContentFromFile(string path)
        {
            ValidateFileExtension(path);

            string content = File.ReadAllText(path);

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
