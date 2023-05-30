using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;
using WorkflowBusinessLogic;

namespace UMP.Loodsman.Adapters
{
    public class WFBLAdapter: ApiAdapterBase
    {
        public IWFBusinessLogic WFBL { get; private set; }
        // public IWFSystem WfSystem { get; private set; }
        public WFBLAdapter(IWFBusinessLogic WFBL) : base((ISimpleAPI2)WFBL.GetSimpleAPIInterface())
        {
            this.WFBL = WFBL;
            // WfSystem = Wfbl.WFSystem;
        }

        // public WfblAdapter(ConnectionAdapter connectionAdapter) : base(connectionAdapter)
        // {
        //
        // }

    }
}
