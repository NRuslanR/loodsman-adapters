using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataProvider;
using Loodsman;

namespace LoodsmanAdapters
{
    public abstract class AdapterBase
    {
        private readonly ConnectionAdapter _connectionAdapter;
        protected AdapterBase(string dbName)
        {
            _connectionAdapter = new ConnectionAdapter(dbName);
            DBName = _connectionAdapter.SimpleApi.DBName;
            Checkout = _connectionAdapter.SimpleApi.Checkout;
            Init();
        }

        protected AdapterBase(ILoodsmanApplication app)
        {
            _connectionAdapter = new ConnectionAdapter(app);
            DBName = _connectionAdapter.SimpleApi.DBName;
            Checkout = _connectionAdapter.SimpleApi.Checkout;
            Init();
        }

        protected virtual void Init()
        {

        }

        public string DBName { get; }
        public string Checkout { get; }

        public T RunMethod<T>(string methodName, params object[] arguments)
        {
            return (T)_connectionAdapter.SimpleApi.RunMethod(methodName, arguments);
        }

        public IDataSet GetDataSet(string methodName, params object[] arguments)
        {
            return _connectionAdapter.SimpleApi.GetDataSet(methodName, arguments) as IDataSet;
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
                (taskId, resultData, dataSet, errorMsg, tag) =>
                {
                    if (errorMsg != null)
                    {
                        throw new Exception(errorMsg);
                    }
                    if (typeof(T) == typeof(IDataSet))
                    {
                        tcs.TrySetResult((T)dataSet);
                    }
                    else
                    {
                        tcs.TrySetResult((T)resultData);
                    }
                };
            try
            {
                pluginCallBack.ResultReceived += handler;
                var taskId = _connectionAdapter.SimpleApi.AsyncTask.Run(methodName, arguments, pluginCallBack, 0);
                token.Register(() =>
                {
                    _connectionAdapter.SimpleApi.AsyncTask.Cancel(taskId);
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
