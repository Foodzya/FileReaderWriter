namespace FileReaderWriter.WriteOptions
{
    public interface IWriteAction
    {
        public void WriteToFile(string content, string targetFile);
    }
}
