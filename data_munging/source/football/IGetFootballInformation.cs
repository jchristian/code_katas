using System.Collections.Generic;

namespace source.football
{
    public interface IGetFootballInformation
    {
        IEnumerable<IProvideTeamInformation> GetAllTheTeams();
    }
}