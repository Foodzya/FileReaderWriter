using System;
using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;

namespace FileReaderWriter.ReadOptions
{
    public class EtxtReader : IFileReader
    {
        public string FormatContent(string content)
        {
            return CaesarCipherDirectionMenu(content);
        }

        private string CaesarCipherDirectionMenu(string content)
        {
            MenuContext menuContext = new MenuContext();

            CaesarEncryptor decryptor = new CaesarEncryptor();

            string decryptionResult = string.Empty;

            int decryptionShift;

            Console.Clear();

            Console.WriteLine("Select decryption direction:\n" +
            "1 -- Left\n" +
            "2 -- Right\n" +
            "3 -- Back to the menu\n");
            
            ConsoleKey direction = Console.ReadKey().Key;

            switch (direction)
            {
                case ConsoleKey.D1:
                    decryptionShift = GetCaesarCipherShift();
                    decryptionResult = decryptor.LeftShiftCipher(content, decryptionShift);
                    break;
                case ConsoleKey.D2:
                    decryptionShift = GetCaesarCipherShift();
                    decryptionResult = decryptor.RightShiftCipher(content, decryptionShift);
                    break;
                case ConsoleKey.D3:
                    menuContext.ChangeMenuState(new ReadMenuState());
                    break;
                default:
                    CaesarCipherDirectionMenu(content);
                    break;
            }

            return decryptionResult;
        }

        private int GetCaesarCipherShift()
        {
            MenuContext menuContext = new MenuContext();
            
            int decryptionShift = 0;

            Console.WriteLine("\nCAESAR CIPHER SHIFT MENU\n" +
            "Specify decryption shift:\n" +
            "Enter Q to go to the previous menu\n");

            string shiftInput = Console.ReadLine();


            if (shiftInput.ToLower() == "q")
                menuContext.ChangeMenuState(new ReadMenuState());

            try
            {
                decryptionShift = int.Parse(shiftInput);
            }
            catch (FormatException e)
            {
                Console.Clear();

                Console.WriteLine(e);

                GetCaesarCipherShift();
            }

            return decryptionShift;
        }
    }
}
