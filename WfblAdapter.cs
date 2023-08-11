using WorkflowBusinessLogic;

namespace UMP.Loodsman.Adapters
{
    public class WfblAdapter : IWfblAdapter
    {
        // public IWFSystem WfSystem { get; private set; } Раскомментить когда найдётся IWFSystem

        public WfblAdapter(string DBName, string serverName) : this(DBName, serverName, "")
        {
        }

        public WfblAdapter(string DBName, string serverName, string checkout) : this(DBName, serverName, checkout, "",
            "")
        {
        }

        public WfblAdapter(string DBName, string serverName, string checkout, string username, string password)
        {
            Wfbl = new WFBusinessLogicClass();
            Wfbl.AppServer = serverName;
            Wfbl.Connected = true;
            Wfbl.ConnectToDB(DBName, "", "", "");
            // WfSystem = Wfbl.WFSystem;
        }

        public WfblAdapter(IWFBusinessLogic wfbl)
        {
            Wfbl = wfbl;
            // WfSystem = Wfbl.WFSystem;
        }

        public IWFBusinessLogic Wfbl { get; }
    }
}