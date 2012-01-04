namespace source.file_parser
{
    public class CreateFileStreams : ICreateFileStreams
    {
        public IProvideFileInformation GetStreamFor(string fileLocation)
        {
            return new FileInformationProvider(fileLocation);
        }
    }
}