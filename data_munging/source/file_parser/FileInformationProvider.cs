using System.IO;

namespace source.file_parser
{
    public class FileInformationProvider : IProvideFileInformation
    {
        StreamReader _fileInfo;

        public FileInformationProvider(string fileLocation)
        {
            _fileInfo = new FileInfo(fileLocation).OpenText();
        }

        public void Dispose()
        {
            _fileInfo.Dispose();
        }
    }
}