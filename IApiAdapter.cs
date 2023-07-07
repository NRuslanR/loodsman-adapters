using System.Threading;
using System.Threading.Tasks;
using DataProvider;

namespace UMP.Loodsman.Adapters
{
    public interface IApiAdapter
    {
        ISimpleAPI2 Api { get; }

        void RunMethod(string methodName, params object[] arguments);
        T RunMethod<T>(string methodName, params object[] arguments);
        IDataSet GetDataSet(string methodName, params object[] arguments);
        Task RunMethodAsync(string methodName, params object[] arguments);
        Task RunMethodAsync(CancellationToken token, string methodName, params object[] arguments);
        Task<T> RunMethodAsync<T>(string methodName, params object[] arguments);

        Task<T> RunMethodAsync<T>(CancellationToken token, string methodName, params object[] arguments);

        Task<IDataSet> GetDataSetAsync(string methodName, params object[] arguments);

        Task<IDataSet> GetDataSetAsync(CancellationToken token, string methodName, params object[] arguments);
    }
}
