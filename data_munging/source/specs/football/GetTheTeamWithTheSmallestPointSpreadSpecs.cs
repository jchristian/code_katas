using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;
using source.football;

namespace source.specs.football
{
    public class GetTheTeamWithTheSmallestPointSpreadSpecs
    {
        public abstract class concern : Observes<IFetchInformation<IProvideTeamInformation>,
                                            GetTheTeamWithTheSmallestPointSpread> {}

        [Subject(typeof(GetTheTeamWithTheSmallestPointSpread))]
        public class when_getting_the_team_with_the_smallest_point_spread : concern
        {
            Establish c = () =>
            {
                the_team_with_the_smallest_point_spread = fake.an<IProvideTeamInformation>();

                var all_the_teams = Enumerable.Range(1, 10).Select(x =>
                {
                    var a_team_with_a_large_point_spread = fake.an<IProvideTeamInformation>();
                    a_team_with_a_large_point_spread.setup(team => team.GetPointSpread()).Return(100);
                    return a_team_with_a_large_point_spread;
                }).ToList();

                all_the_teams.Add(the_team_with_the_smallest_point_spread);
                
                var football_repository = depends.on<IGetFootballInformation>();
                
                football_repository.setup(x => x.GetAllTheTeams()).Return(all_the_teams);
                the_team_with_the_smallest_point_spread.setup(x => x.GetPointSpread()).Return(10);
            };

            Because of = () =>
                the_returned_team = sut.Fetch();

            It should_return_the_team_from_the_repository_with_the_smallest_point_spread = () =>
                the_returned_team.ShouldEqual(the_team_with_the_smallest_point_spread);

            static IProvideTeamInformation the_returned_team;
            static IProvideTeamInformation the_team_with_the_smallest_point_spread;
        }
    }
}
