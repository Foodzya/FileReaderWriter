using System;
using System.IO;
using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;

namespace FileReaderWriter.WriteOptions
{
    public class ETxtWriter : IWriteAction
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
                    Console.WriteLine(e.Message + "\nPlease specify txt file for writing there");
                }
            }
        }

        private string CipherDirectionMenu(string content)
        {
            Console.Clear();

            Console.WriteLine("Select encription direction:\n" +
            "1 -- Left\n" +
            "2 -- Right\n" +
            "3 -- Back to the menu\n");

            MenuContext menuContext = new MenuContext();

            ConsoleKey direction = Console.ReadKey().Key;

            string result = string.Empty;

            int shift;

            switch (direction)
            {
                case ConsoleKey.D1:
                    shift = GetCipherShift();
                    result = LeftShiftCipher(content, shift);
                    break;
                case ConsoleKey.D2:
                    shift = GetCipherShift();
                    result = RightShiftCipher(content, shift);
                    break;
                case ConsoleKey.D3:
                    menuContext.ChangeMenuState(new WriteMenuState());
                    break;
                default: 
                    Console.WriteLine("An error occured");
                    CipherDirectionMenu(content);
                    break;                    
            }

            return result;
        }

        private int GetCipherShift()
        {
            MenuContext menuContext = new MenuContext();

            Console.WriteLine("\nCIPHER SHIFT MENU\n" +
            "Specify encription shift:\n" + 
            "Enter Q to go to the previous menu\n");
            
            string shiftInput = Console.ReadLine();

            if (shiftInput.ToLower() == "q")
                menuContext.ChangeMenuState(new WriteMenuState());
                
            int shift = 0;

            try 
            {
                shift = Int32.Parse(shiftInput);
            }
            catch(FormatException e)
            {
                Console.Clear();
                
                Console.WriteLine(e);

                GetCipherShift();
            }

           return shift;
        }

        private static char Cipher(char ch, int shift)
        {
            if (!char.IsLetter(ch))
                return ch;

            char offset = char.IsUpper(ch) ? 'A' : 'a';
            
            return (char)((((ch + shift) - offset) % 26) + offset);
        }

        public static string RightShiftCipher(string content, int shift)
        {
            string output = string.Empty;

            foreach (char ch in content)
                output += Cipher(ch, shift);

            return output;
        }

        public static string LeftShiftCipher(string content, int shift)
        {
            return RightShiftCipher(content, 26 - shift);
        }
    }
}
