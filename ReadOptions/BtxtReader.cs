using FileReaderWriter.Menu;
using FileReaderWriter.Menu.MenuStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaderWriter.ReadOptions
{
    public class BtxtReader : IFileReader
    {
        public string FormatContent(string content)
        {
            string formattedContent;

            try 
            {
                formattedContent = GetStringFromBytes(content); 

                return formattedContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return string.Empty;
        }

        private string GetStringFromBytes(string content) 
        {
            List<byte> byteList = new List<byte>();

            StringBuilder sb = new StringBuilder();
            
            foreach (char c in content)
            {
                if (c == '0' || c == '1')
                    sb.Append(c);
                else if (char.IsWhiteSpace(c))
                    continue;
                else
                {
                    Console.WriteLine("\aBtxt file must contain 0 and 1 only. \n" +
                        "Please check out content of the source file\n" +
                        "Press any button to continue");

                    Console.ReadKey();

                    MenuContext menuContext = new MenuContext();

                    menuContext.ChangeMenuState(new MainMenuState());
                }                                        
            }

            string noSpaceBtxtContent = sb.ToString();

            byte convertedItem;

            for (int i = 0; i < sb.Length; i += 8)
            {
                convertedItem = Convert.ToByte(noSpaceBtxtContent.Substring(i, 8), 2);

                byteList.Add(convertedItem);
            }

            string convertedContent = Encoding.UTF8.GetString(byteList.ToArray());

            return convertedContent;
        }
    }
}
