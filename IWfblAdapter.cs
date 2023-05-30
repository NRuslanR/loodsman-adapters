using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowBusinessLogic;

namespace UMP.Loodsman.Adapters
{
    public interface IWfblAdapter
    {
        IWFBusinessLogic Wfbl { get; }
    }
}
