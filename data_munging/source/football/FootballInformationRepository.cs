using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Machine.Specifications.Utility;

namespace source.football
{
    public class FootballInformationRepository : IGetFootballInformation
    {
        public IEnumerable<IProvideTeamInformation> GetAllTheTeams()
        {
            using (var reader = new FileInfo(@"data\football.dat").OpenText())
            {
                string line;

                Enumerable.Range(1, 5).Each(x => reader.ReadLine());
                while ((line = reader.ReadLine()) != null)
                {
                    if(IsValidLine(line))
                        yield return ParseLine(line);
                }
            }
        }

        bool IsValidLine(string line)
        {
            return line.Split(' ').Where(x => x != "").Count() > 7;
        }

        IProvideTeamInformation ParseLine(string line)
        {
            var values = line.Split(' ').Where(x => x != "").ToArray();

            return new Team(values[1], Convert.ToInt32(values[6]), Convert.ToInt32(values[8]));
        }
    }
}