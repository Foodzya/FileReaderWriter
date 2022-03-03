namespace FileReaderWriter.TextManipulations
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

        private char Cipher(char currentChar, int shift)
        {
            const int totalNumOfLetters = 26;

            if (!char.IsLetter(currentChar))
                return currentChar;

            // Checks if char is upper to consider where to start from.
            char offset = char.IsUpper(currentChar) ? 'A' : 'a';

            int ASDASD = (((currentChar + shift) - offset) % totalNumOfLetters);

            // First, shift is applied to current char, then offset is subtracted and divided into
            // total number of letters in alphabet to check for 'Z' chars, because alphabet is over.
            // Then we add offset again.
            char shiftedLetter = (char)((((currentChar + shift) - offset) % totalNumOfLetters) + offset);

            return shiftedLetter;
        }
    }
}