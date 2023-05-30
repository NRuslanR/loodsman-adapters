using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;
using WorkflowBusinessLogic;

namespace UMP.Loodsman.Adapters
{
    public class WfblAdapter: ApiAdapterBase
    {
        public IWFBusinessLogic Wfbl { get; private set; }
        // public IWFSystem WfSystem { get; private set; }
        public WfblAdapter(IWFBusinessLogic wfbl) : base((ISimpleAPI2)wfbl.GetSimpleAPIInterface())
        {
            Wfbl = wfbl;
            // WfSystem = Wfbl.WFSystem;
        }

        // public WfblAdapter(ConnectionAdapter connectionAdapter) : base(connectionAdapter)
        // {
        //
        // }

    }
}
