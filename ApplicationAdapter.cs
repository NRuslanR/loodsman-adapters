using Loodsman;
using SUPR;

namespace UMP.Loodsman.Adapters
{
    public class ApplicationAdapter
    {
        private readonly ILoodsmanApplication _app;
        public ApplicationAdapter(ILoodsmanApplication app)
        {
            _app = app;
        }
    }
}
