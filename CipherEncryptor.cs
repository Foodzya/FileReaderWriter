namespace FileReaderWriter
{
    public class CipherEncryptor
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
            return RightShiftCipher(content, 26 - shift);
        }

        private char Cipher(char ch, int shift)
        {
            if (!char.IsLetter(ch))
                return ch;

            char offset = char.IsUpper(ch) ? 'A' : 'a';

            return (char)((((ch + shift) - offset) % 26) + offset);
        }

    }
}