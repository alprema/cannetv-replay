using System.Data;

namespace CanneTVReplay.Helpers
{
    public interface IConnectionProvider
    {
        public IDbConnection Connection { get; }
    }
}
