using CanneTVReplay.Helpers;
using Dapper;
using System.Data;

namespace CanneTVReplay.Repositories
{
    public class CompetitionRepository
    {
        private IDbConnection _canneCounterDb;

        public CompetitionRepository(CanneCounterConnectionProvider canneCounterConnectionProvider)
        {
            _canneCounterDb = canneCounterConnectionProvider.Connection;
        }

        public IList<Competition> GetCompetitions(int[] competitionIds)
        {
            return _canneCounterDb
                .Query<Competition>(@"
                    SELECT
                        id,
                        name,
                        started as startDate,
                        ended as endDate
                    FROM encounters WHERE id IN @competitionIds ORDER BY id", new { competitionIds }
                )
                .ToList();
        }
    }
}
