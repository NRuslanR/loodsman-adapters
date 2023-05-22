using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;

namespace LoodsmanAdapters
{
    public class PluginCallBack: IPluginCallBack
    {
        public delegate void CallBackHandler(int taskId, object resultData, DataSet dataSet, string errorMsg, int tag);

        public event CallBackHandler ResultReceived;
        public void CallBackProc(int TaskID, object ResultData, DataSet DataSet, string ErrorMsg, int Tag)
        {
            ResultReceived?.Invoke(TaskID, ResultData, DataSet, ErrorMsg, Tag);
        }
    }
}
