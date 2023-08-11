using DataProvider;

namespace UMP.Loodsman.Adapters
{
    public class PluginCallBack : IPluginCallBack
    {
        public delegate void CallBackHandler(int taskId, object resultData, IDataSet DataSet, string errorMsg, int tag);

        public void CallBackProc(int TaskID, object ResultData, DataSet DataSet, string ErrorMsg, int Tag)
        {
            ResultReceived?.Invoke(TaskID, ResultData, DataSet, ErrorMsg, Tag);
        }

        public event CallBackHandler ResultReceived;
    }
}