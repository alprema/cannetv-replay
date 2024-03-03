using CanneTVReplay.Helpers;
using Dapper;
using Org.BouncyCastle.Security;
using System.Data;
using System.Text.Json;

namespace CanneTVReplay.Repositories
{
    public class EncounterRepository
    {
        private class EncounterRow
        {
            public string TeamName { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public int Counter { get; set; }
            public string TechnicalCharacteristics { get; set; }    
        }

        private class TechnicalCharateristics
        {
            public string? assaultduration { get; set; }
            public string? roundperassault { get; set; }
            public string? roundduration { get; set; }
        }

        private IDbConnection _canneCounterDb;

        private const int RED_TEAM_COUNTER = 101;
        private const int BLUE_TEAM_COUNTER = 102;

        public EncounterRepository(CanneCounterConnectionProvider canneCounterConnectionProvider)
        {
            _canneCounterDb = canneCounterConnectionProvider.Connection;
        }

        public Encounter GetEncounter(int encounterId)
        {
            
            var rows = _canneCounterDb
                .Query<EncounterRow>(@"
                    SELECT teams.username as TeamName, members.first_name as Firstname, members.last_name as Lastname, counter as Counter, encounters.technical_characteristics as TechnicalCharacteristics
                    FROM `logs`
                    join users as teams on logs.user_id = teams.id
                    join compositions on teams.id = compositions.team_id
                    join users as members on compositions.user_id = members.id
                    join encounters on logs.encounter_id = encounters.id
                    where logs.encounter_id = @encounterId and logs.logtype_id = 200 and counter in (101, 102)", new { encounterId }
                )
                .ToList();

            var rowsByCounter = rows.GroupBy(x => x.Counter);

            var technicalCharacteristics = JsonSerializer.Deserialize<TechnicalCharateristics>(rows.First().TechnicalCharacteristics);
            var durationInSeconds = technicalCharacteristics.assaultduration != null
                ? int.Parse(technicalCharacteristics.assaultduration)
                : int.Parse(technicalCharacteristics.roundperassault) * int.Parse(technicalCharacteristics.roundduration);


            return new Encounter
            {
                Id = encounterId,
                DurationInSeconds = durationInSeconds,

                RedTeam = new Team
                {
                    Name = rowsByCounter.Where(x => x.Key == RED_TEAM_COUNTER).SelectMany(x => x).First().TeamName,
                    Members = rowsByCounter
                                .Where(x => x.Key == RED_TEAM_COUNTER)
                                .SelectMany(grp => grp)
                                .Select(row => new Fighter
                                {
                                    FirstName = row.Firstname,
                                    LastName = row.Lastname,
                                }
                                ).ToList(),
                },
                BlueTeam = new Team
                {
                    Name = rowsByCounter.Where(x => x.Key == BLUE_TEAM_COUNTER).SelectMany(x => x).First().TeamName,
                    Members = rowsByCounter
                                .Where(x => x.Key == BLUE_TEAM_COUNTER)
                                .SelectMany(grp => grp)
                                .Select(row => new Fighter
                                {
                                    FirstName = row.Firstname,
                                    LastName = row.Lastname,
                                }
                                ).ToList(),
                }
            };             
        }

        public IList<EncounterSummary> ListEncountersForCompetition(int competitionId)
        {

            
            var summaries = _canneCounterDb.Query<EncounterSummary>(@"
                SELECT child.id, 
                (            
                    SELECT group_concat(teams.username separator ' - ') as team_name
                    FROM `logs`
                    join users as teams on logs.user_id = teams.id
                    join encounters on logs.encounter_id = encounters.id
                    where logs.encounter_id = child.id and logs.logtype_id = 200 and counter in (101, 102)
                ) name,    
                mid(mid(child.technical_characteristics, locate('""area"":""', child.technical_characteristics), 10), 9,1) area,
                (
                    select created from logs where logs.encounter_id = child.id and logs.logtype_id = 310 order by created asc limit 1
                ) as starttime
                FROM `encounters` as child
                join `encounters` as parent on child.lft BETWEEN parent.lft and parent.rght
                where parent.id = @competitionId and parent.encountertype_id = 4 and child.encountertype_id = 1
                order by (select UNIX_TIMESTAMP(starttime) DIV 60), area;
            ", new { competitionId }).ToList();


            foreach (var summary in summaries)
            {
                summary.StartTime = TimeZoneInfo.ConvertTimeFromUtc(summary.StartTime, TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"));
            }

            return summaries;
        }
    }
}
