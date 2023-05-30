using DataProvider;
using Loodsman;
using LoodsmanObjects;
using SUPR;
using Task = System.Threading.Tasks.Task;

namespace UMP.Loodsman.Adapters
{
    public class ConnectionAdapter
    {
        public ISimpleAPI2 Api { get; }

        public ConnectionAdapter(string dbName)
        {
            var connection = new LoodsmanConnectionClass();
            connection.API8.UniConnect(dbName, "");
            Api = (ISimpleAPI2)connection.API8.GetSimpleAPI();
        }

        public ConnectionAdapter(ISimpleAPI2 simpleApi)
        {
            Api = simpleApi;
        }
    }
}
