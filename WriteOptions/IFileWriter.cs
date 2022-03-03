namespace FileReaderWriter.WriteOptions
{
    public interface IFileWriter
    {
        public void WriteToFile(string content, string targetFile);
    }
}
