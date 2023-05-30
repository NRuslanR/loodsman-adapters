using Loodsman;
using SUPR;

namespace UMP.Loodsman.Adapters
{
    public class ApplicationAdapter: IApplicationAdapter
    {
        public ILoodsmanApplication Application { get; private set; }

        public ApplicationAdapter(ILoodsmanApplication application)
        {
            Application = application;
        }
    }
}
