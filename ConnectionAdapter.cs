using DataProvider;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Loodsman;

namespace LoodsmanAdapters
{
    internal class ConnectionAdapter
    {
        public ISimpleAPI SimpleApi { get; }
        public ConnectionAdapter(string dbName)
        {
            var _connection = new LoodsmanConnectionClass();
            _connection.API8.UniConnect(dbName, "");
            SimpleApi = _connection.API8.GetSimpleAPI();
        }

        public ConnectionAdapter(ILoodsmanApplication app)
        {
            SimpleApi = app.DataBase.Connection;
        }
    }
}
