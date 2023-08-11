using DataProvider;
using SUPR;

namespace UMP.Loodsman.Adapters
{
    public class WbsSystemAdapter : IWbsSystemAdapter
    {
        public WbsSystemAdapter(ISimpleAPI2 simpleApi)
        {
            WbsSystem = new WBSSystemClass();
            WbsSystem.Connection = simpleApi;
        }

        public WbsSystemAdapter(IWBSSystem wbsSystem)
        {
            WbsSystem = wbsSystem;
        }

        public IWBSSystem WbsSystem { get; }
    }
}