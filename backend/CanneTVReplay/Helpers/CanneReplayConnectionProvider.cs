using System.Data;

namespace CanneTVReplay.Helpers
{
    public class CanneReplayConnectionProvider : IConnectionProvider
    {
        public IDbConnection Connection { get; init; }
    }
}
