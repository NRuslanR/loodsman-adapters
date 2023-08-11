using Loodsman;

namespace UMP.Loodsman.Adapters
{
    public interface IApplicationAdapter
    {
        ILoodsmanApplication Application { get; }
        void OpenObjectsInNewWindow(string objectIds, string checkoutName = null);
    }
}