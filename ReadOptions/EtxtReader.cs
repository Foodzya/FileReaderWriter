using System;
using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;
using FileReaderWriter.TextManipulations;

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

            string decryptResult = string.Empty;

            int decryptionShift;

            Console.Clear();

            Console.WriteLine("Select decryption direction:\n" +
            "1 -- Left\n" +
            "2 -- Right\n" +
            "3 -- Back to the menu\n");
            
            ConsoleKey decryptDirection = Console.ReadKey().Key;

            switch (decryptDirection)
            {
                case ConsoleKey.D1:
                    decryptionShift = GetCaesarCipherShift();
                    decryptResult = decryptor.LeftShiftCipher(content, decryptionShift);
                    break;
                case ConsoleKey.D2:
                    decryptionShift = GetCaesarCipherShift();
                    decryptResult = decryptor.RightShiftCipher(content, decryptionShift);
                    break;
                case ConsoleKey.D3:
                    menuContext.ChangeMenuState(new ReadMenuState());
                    break;
                default:
                    CaesarCipherDirectionMenu(content);
                    break;
            }

            return decryptResult;
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
