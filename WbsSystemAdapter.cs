using DataProvider;
using SUPR;

namespace UMP.Loodsman.Adapters
{
    public class WbsSystemAdapter : ApiAdapterBase
    {
        public IWBSSystem WbsSystem { get; private set; }
        public WbsSystemAdapter(IWBSSystem wbsSystem) : base((ISimpleAPI2)wbsSystem.Connection)
        {
            WbsSystem = wbsSystem;
        }
        public WbsSystemAdapter(ConnectionAdapter connectionAdapter) : base(connectionAdapter)
        {
            WbsSystem = new WBSSystemClass()
            {
                Connection = connectionAdapter.Api
            };
        }
    }
}
