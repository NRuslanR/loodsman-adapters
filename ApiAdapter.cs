
using DataProvider;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace UMP.Loodsman.Adapters
{
    public class ApiAdapter: IApiAdapter
    {
        public ISimpleAPI2 Api { get; private set; }
        public ApiAdapter(ISimpleAPI2 api)
        {
            Api = api;
        }
        public T RunMethod<T>(string methodName, params object[] arguments)
        {
            return (T)Api.RunMethod(methodName, arguments);
        }

        public IDataSet GetDataSet(string methodName, params object[] arguments)
        {
            return Api.GetDataSet(methodName, arguments) as IDataSet;
        }

        public Task RunMethodAsync(string methodName, params object[] arguments)
        {
            return RunTaskAsync<object>(CancellationToken.None, methodName, arguments);
        }

        public Task RunMethodAsync(CancellationToken token, string methodName, params object[] arguments)
        {
            return RunTaskAsync<object>(token, methodName, arguments);
        }

        public Task<T> RunMethodAsync<T>(string methodName, params object[] arguments)
        {
            return RunTaskAsync<T>(CancellationToken.None, methodName, arguments);
        }

        public Task<T> RunMethodAsync<T>(CancellationToken token, string methodName, params object[] arguments)
        {
            return RunTaskAsync<T>(token, methodName, arguments);
        }

        public Task<IDataSet> GetDataSetAsync(string methodName, params object[] arguments)
        {
            return RunTaskAsync<IDataSet>(CancellationToken.None, methodName, arguments);
        }

        public Task<IDataSet> GetDataSetAsync(CancellationToken token, string methodName, params object[] arguments)
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
                var taskId = Api.AsyncTask.Run(methodName, arguments, pluginCallBack, 0);
                token.Register(() =>
                {
                    Api.AsyncTask.Cancel(taskId);
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
