using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;
using SUPR;

namespace LoodsmanAdapters
{
    public class ApiAdapter: AdapterBase
    {
        private readonly ConnectionAdapter _connectionAdapter;
        public ApiAdapter(string dbName): base(dbName)
        {
            _connectionAdapter = new ConnectionAdapter(dbName);
        }

        protected override void Init()
        {
            
        }
    }
}
