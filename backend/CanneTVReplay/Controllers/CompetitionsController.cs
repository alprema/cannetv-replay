using CanneTVReplay.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CanneTVReplay.Controllers
{
    [ApiController]
    [Route("competitions")]
    public class CompetitionsController
    {
        private readonly CompetitionRepository _competitionRepository;
        private readonly EncounterRepository _encounterRepository;

        public CompetitionsController(CompetitionRepository competitionRepository, EncounterRepository encounterRepository)
        {
            _competitionRepository = competitionRepository;
            _encounterRepository = encounterRepository;
        }

        [HttpGet]
        public IList<Competition> Get()
        {
            var competitionsWithVideo = new[] { 2, 1294, 1746, 2439, 3215, 3513 };
            return _competitionRepository.GetCompetitions(competitionsWithVideo);
        }


        [HttpGet]
        [Route("{competitionId}/encounters")]
        public IList<EncounterSummary> List(int competitionId)
        {
            return _encounterRepository.ListEncountersForCompetition(competitionId);
        }
    }
}
