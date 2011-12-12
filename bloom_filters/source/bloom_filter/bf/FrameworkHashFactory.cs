namespace prep.bf
{
  public class FrameworkHashFactory : ICreateAnSingleHash
  {
    public int create_for(string value)
    {
      return value.GetHashCode();
    }
  }
}