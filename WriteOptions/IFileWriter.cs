using System.Threading.Tasks;

namespace FileReaderWriter.WriteOptions
{
    public interface IFileWriter
    {
        public Task WriteToFileAsync(string content, string targetFile);
    }
}
