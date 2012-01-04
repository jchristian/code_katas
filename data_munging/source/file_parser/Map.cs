namespace source.file_parser
{
    public delegate OutType Map<InType, OutType>(InType itemToMap);

    public delegate object Map(object input);
}