using System.Data;

namespace CanneTVReplay.Helpers
{
    public class CanneCounterConnectionProvider : IConnectionProvider
    {
        public IDbConnection Connection { get; init; }
    }
}
