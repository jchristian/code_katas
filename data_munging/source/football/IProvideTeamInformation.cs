namespace source.football
{
    public interface IProvideTeamInformation
    {
        string Name { get; }

        int GetPointSpread();
    }
}