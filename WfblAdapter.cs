using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;
using WorkflowBusinessLogic;

namespace UMP.Loodsman.Adapters
{
    public class WfblAdapter: IWfblAdapter
    {
        public IWFBusinessLogic Wfbl { get; private set; }
        // public IWFSystem WfSystem { get; private set; } Раскомментить когда найдётся IWFSystem

        public WfblAdapter(string DBName): this(DBName, "")
        {

        }

        public WfblAdapter(string DBName, string checkout): this(DBName, checkout, "", "")
        {
            
        }
        public WfblAdapter(string DBName, string checkout, string username, string password)
        {
            Wfbl = new WFBusinessLogicClass();
            Wfbl.ConnectToDB(DBName, "", "", "");
            // WfSystem = Wfbl.WFSystem;
        }

        public WfblAdapter(IWFBusinessLogic wfbl)
        {
            Wfbl = wfbl;
            // WfSystem = Wfbl.WFSystem;
        }
    }
}
