using DataProvider;
using SUPR;

namespace UMP.Loodsman.Adapters
{
    public class WBSSystemAdapter : ApiAdapterBase
    {
        public IWBSSystem WBSSystem { get; private set; }
        public WBSSystemAdapter(IWBSSystem WBSSystem) : base((ISimpleAPI2)WBSSystem.Connection)
        {
            this.WBSSystem = WBSSystem;
        }
        public WBSSystemAdapter(ConnectionAdapter connectionAdapter) : base(connectionAdapter)
        {
            WBSSystem = new WBSSystemClass()
            {
                Connection = connectionAdapter.Api
            };
        }
    }
}
