using System;
using System.Threading;
using System.Threading.Tasks;
using DataProvider;

namespace UMP.Loodsman.Adapters
{
    public class ApiAdapter : IApiAdapter
    {
        public ApiAdapter(ISimpleAPI2 api)
        {
            Api = api;
        }

        public ISimpleAPI2 Api { get; }

        public void RunMethod(string methodName, params object[] arguments)
        {
            Api.RunMethod(methodName, arguments);
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
            var tcs = new TaskCompletionSource<T>();

            void OnResultReceivedEventHandler(int taskId, object resultData, IDataSet dataSet, string errorMsg, int tag)
            {
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    tcs.TrySetException(new Exception(errorMsg));
                }

                else
                {
                    var result = typeof(T) == typeof(IDataSet) ? (T)dataSet : (T)resultData;

                    tcs.TrySetResult(result);
                }
            }

            var pluginCallBack = new PluginCallBack();

            try
            {
                pluginCallBack.ResultReceived += OnResultReceivedEventHandler;
                var taskId = Api.AsyncTask.Run(methodName, arguments, pluginCallBack, 0);
                token.Register(() =>
                {
                    Api.AsyncTask.Cancel(taskId);
                    pluginCallBack.ResultReceived -= OnResultReceivedEventHandler;
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