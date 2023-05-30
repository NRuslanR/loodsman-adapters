using System;
using System.Runtime.InteropServices;
using Loodsman;

namespace UMP.Loodsman.Adapters
{
    public interface IApplicationAdapter
    {
        ILoodsmanApplication Application { get; }
    }
}
