using MoreLinq;

namespace source.football
{
    public class GetTheTeamWithTheSmallestPointSpread : IFetchInformation<IProvideTeamInformation>
    {
        IGetFootballInformation _footballInformationRepository;

        public GetTheTeamWithTheSmallestPointSpread(IGetFootballInformation footballInformationRepository)
        {
            _footballInformationRepository = footballInformationRepository;
        }

        public IProvideTeamInformation Fetch()
        {
            return _footballInformationRepository.GetAllTheTeams().MinBy(x => x.GetPointSpread());
        }
    }
}