using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loodsman;
using SUPR;

namespace LoodsmanAdapters
{
    public class ApplicationAdapter: AdapterBase
    {
        public IPluginCall PluginCall => Application.GetPluginCall();

        public IWBSSystem WbsSystem => PluginCall.WBSSystem;

        public ILoodsmanApplication Application { get; }
        public ApplicationAdapter(ILoodsmanApplication app) : base(app)
        {
            Application = app;
        }

        protected override void Init()
        {
            
        }
    }
}
