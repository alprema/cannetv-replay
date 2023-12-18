using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using CanneTVReplay.Helpers;

namespace CanneTVReplay.Controllers
{
    public class HelpTopic
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [ApiController]
    [Route("db")]
    public class DbInfoController : Controller
    {
        private readonly IDbConnection _canneCounterDb;
        private readonly IDbConnection _canneReplayDb;

        public DbInfoController(CanneCounterConnectionProvider canneCounterConnectionProvider, CanneReplayConnectionProvider canneReplayConnectionProvider)
        {
            _canneCounterDb = canneCounterConnectionProvider.Connection;
            _canneReplayDb = canneReplayConnectionProvider.Connection;
        }

        [HttpGet("canne_counter")]
        public IEnumerable<string> GetEncounters()
        {
            var result = _canneCounterDb.Query<HelpTopic>(
                "SELECT id, name FROM encounters where parent_id = 1;"
            );

            foreach (var helpTopic in result)
            {
                yield return helpTopic.Name;
            }
        }


        [HttpGet("canne_replay")]
        public IEnumerable<string> GetStuff()
        {
            var result = _canneReplayDb.Query<HelpTopic>(
                "SELECT help_category_id as id, name FROM help_category;"
            );

            foreach (var helpTopic in result)
            {
                yield return helpTopic.Name;
            }
        }


    }
}
