using SUPR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMP.Loodsman.Adapters
{
    public interface IWbsSystemAdapter
    {
        IWBSSystem WbsSystem { get; }
    }
}
