
using System;

namespace source.football
{
    public class Team : IProvideTeamInformation
    {
        int _pointsFor;
        int _pointsAgainst;

        public string Name { get; private set; }

        public Team(string name, int pointsFor, int pointsAgainst)
        {
            _pointsFor = pointsFor;
            _pointsAgainst = pointsAgainst;
            Name = name;
        }

        public int GetPointSpread()
        {
            return Math.Abs(_pointsFor - _pointsAgainst);
        }
    }
}