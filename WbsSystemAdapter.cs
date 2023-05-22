using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUPR;

namespace LoodsmanAdapters
{
    public class WbsSystemAdapter : AdapterBase
    {
        private readonly ConnectionAdapter _connectionAdapter;
        public IWBSSystem WbsSystem { get; private set; }

        public WbsSystemAdapter(string dbName) : base(dbName)
        {
        }

        protected override void Init()
        {
            WbsSystem = new WBSSystemClass()
            {
                Connection = _connectionAdapter.SimpleApi
            };
        }
    }
}
