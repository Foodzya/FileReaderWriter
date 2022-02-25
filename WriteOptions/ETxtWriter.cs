using System;
using System.IO;
using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;
using FileReaderWriter;

namespace FileReaderWriter.WriteOptions
{
    public class EtxtWriter : IWriteAction
    {
        public void WriteToFile(string content, string targetFile)
        {
            string result = CipherDirectionMenu(content);

            if (result != null)
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(targetFile))
                    {
                        sw.WriteLine(result);
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message + "\nPlease specify etxt file for writing there");
                }
            }
        }

        private string CipherDirectionMenu(string content)
        {
            MenuContext menuContext = new MenuContext();

            CaesarEncryptor encryptor = new CaesarEncryptor();

            string result = string.Empty;

            int shift;

            Console.Clear();

            Console.WriteLine("Select encription direction:\n" +
            "1 -- Left\n" +
            "2 -- Right\n" +
            "3 -- Back to the menu\n");

            ConsoleKey direction = Console.ReadKey().Key;

            switch (direction)
            {
                case ConsoleKey.D1:
                    shift = GetCipherShift();
                    result = encryptor.LeftShiftCipher(content, shift);
                    break;
                case ConsoleKey.D2:
                    shift = GetCipherShift();
                    result = encryptor.RightShiftCipher(content, shift);
                    break;
                case ConsoleKey.D3:
                    menuContext.ChangeMenuState(new WriteMenuState());
                    break;
                default:
                    Console.WriteLine("An error occured\n" +
                        "Try again..\n" +
                        "To continue press any button.");
                    Console.ReadKey();
                    CipherDirectionMenu(content);
                    break;
            }

            return result;
        }

        private int GetCipherShift()
        {
            MenuContext menuContext = new MenuContext();

            int shift = 0;

            Console.WriteLine("\nCIPHER SHIFT MENU\n" +
            "Specify encription shift:\n" +
            "Enter Q to go to the previous menu\n");

            string shiftInput = Console.ReadLine();

            if (shiftInput.ToLower() == "q")
                menuContext.ChangeMenuState(new WriteMenuState());

            try
            {
                shift = Int32.Parse(shiftInput);
            }
            catch (FormatException e)
            {
                Console.Clear();

                Console.WriteLine(e);

                GetCipherShift();
            }

            return shift;
        }
    }
}
