using WorkflowBusinessLogic;

namespace UMP.Loodsman.Adapters
{
    public interface IWfblAdapter
    {
        IWFBusinessLogic Wfbl { get; }
    }
}