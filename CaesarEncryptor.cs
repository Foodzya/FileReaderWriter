namespace FileReaderWriter
{
    public class CaesarEncryptor
    {
        public string RightShiftCipher(string content, int shift)
        {
            string output = string.Empty;

            foreach (char ch in content)
                output += Cipher(ch, shift);

            return output;
        }

        public string LeftShiftCipher(string content, int shift)
        {
            const int totalNumOfLetters = 26;

            return RightShiftCipher(content, totalNumOfLetters - shift);
        }

        private char Cipher(char ch, int shift)
        {
            const int totalNumOfLetters = 26;

            if (!char.IsLetter(ch))
                return ch;

            char offset = char.IsUpper(ch) ? 'A' : 'a';

            return (char)((((ch + shift) - offset) % totalNumOfLetters) + offset);
        }
    }
}