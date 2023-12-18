using CanneTVReplay.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CanneTVReplay.Controllers
{
    [ApiController]
    [Route("encounters")]
    public class EncountersController
    {
        private readonly EncounterRepository _encounterRepository;

        public EncountersController(EncounterRepository encounterRepository)
        {
            _encounterRepository = encounterRepository;
        }

        [HttpGet]
        [Route("{encounterId}")]
        public Encounter Get(int encounterId)
        {
            return _encounterRepository.GetEncounter(encounterId);
        }
    }
}
