namespace source.file_parser
{
    public interface ICreateFileStreams
    {
        IProvideFileInformation GetStreamFor(string fileLocation);
    }
}