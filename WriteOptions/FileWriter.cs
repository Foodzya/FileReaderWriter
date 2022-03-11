using System;
using System.IO;
using System.Threading.Tasks;
using FileReaderWriter.Extensions;
using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;
using static FileReaderWriter.Enums.FileFormatEnum;

namespace FileReaderWriter.WriteOptions
{
    public class FileWriter
    {
        private IFileWriter _writer;

        public async Task WriteToFileAsync(string content, string targetFile)
        {
            SetWriterByFileExtension(targetFile);

            await _writer.WriteToFileAsync(content, targetFile);
        }

        private void SetWriter(IFileWriter writer)
        {
            _writer = writer;
        }

        private void SetWriterByFileExtension(string path)
        {
            string extension = Path.GetExtension(path);

            switch (extension)
            {
                case string format when format.Equals(FileFormat.txt.ToValidFileFormat()):
                    SetWriter(new TxtWriter());
                    break;
                case string format when format.Equals(FileFormat.rtxt.ToValidFileFormat()):
                    SetWriter(new RtxtWriter());
                    break;
                case string format when format.Equals(FileFormat.etxt.ToValidFileFormat()):
                    SetWriter(new EtxtWriter());
                    break;
                case string format when format.Equals(FileFormat.btxt.ToValidFileFormat()):
                    SetWriter(new BtxtWriter());
                    break;
                default:
                    Console.WriteLine("This file type is unsupported.\n" +
                        "Press anything to continue..");
                    Console.ReadKey();
                    MenuContext menuContext = new MenuContext();
                    menuContext.ChangeMenuState(new MainMenuState());
                    break;
            }
        }
    }
}