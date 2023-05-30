using System;
using System.Threading;
using System.Threading.Tasks;
using DataProvider;
using Loodsman;

namespace UMP.Loodsman.Adapters
{
    public abstract class ApiAdapterBase
    {
        private readonly ConnectionAdapter _connectionAdapter;
        protected ApiAdapterBase(ConnectionAdapter connectionAdapter)
        {
            _connectionAdapter = connectionAdapter;
        }

        protected ApiAdapterBase(ISimpleAPI2 simpleApi)
        {
            _connectionAdapter = new ConnectionAdapter(simpleApi);
        }

        protected T RunMethod<T>(string methodName, params object[] arguments)
        {
            return (T)_connectionAdapter.Api.RunMethod(methodName, arguments);
        }

        protected IDataSet GetDataSet(string methodName, params object[] arguments)
        {
            return _connectionAdapter.Api.GetDataSet(methodName, arguments) as IDataSet;
        }

        protected Task<T> RunMethodAsync<T>(string methodName, params object[] arguments)
        {
            return RunTaskAsync<T>(CancellationToken.None, methodName, arguments);
        }

        protected Task<T> RunMethodAsync<T>(CancellationToken token, string methodName, params object[] arguments)
        {
            return RunTaskAsync<T>(token, methodName, arguments);
        }

        protected Task<IDataSet> GetDataSetAsync(string methodName, params object[] arguments)
        {
            return RunTaskAsync<IDataSet>(CancellationToken.None, methodName, arguments);
        }
        protected Task<IDataSet> GetDataSetAsync(CancellationToken token, string methodName, params object[] arguments)
        {
            return RunTaskAsync<IDataSet>(token, methodName, arguments);
        }

        private Task<T> RunTaskAsync<T>(CancellationToken token, string methodName, params object[] arguments)
        {
            var pluginCallBack = new PluginCallBack();
            var tcs = new TaskCompletionSource<T>();
            PluginCallBack.CallBackHandler handler =
                (taskId, resultData, IDataSet, errorMsg, tag) =>
                {
                    if (errorMsg != null)
                    {
                        throw new Exception(errorMsg);
                    }
                    if (typeof(T) == typeof(IDataSet))
                    {
                        tcs.TrySetResult((T)IDataSet);
                    }
                    else
                    {
                        tcs.TrySetResult((T)resultData);
                    }
                };
            try
            {
                pluginCallBack.ResultReceived += handler;
                var taskId = _connectionAdapter.Api.AsyncTask.Run(methodName, arguments, pluginCallBack, 0);
                token.Register(() =>
                {
                    _connectionAdapter.Api.AsyncTask.Cancel(taskId);
                    pluginCallBack.ResultReceived -= handler;
                    tcs.SetCanceled();
                });
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
            return tcs.Task;
        }
    }
}
