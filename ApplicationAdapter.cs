using System;
using System.Runtime.InteropServices;
using Loodsman;

namespace UMP.Loodsman.Adapters
{
    public class ApplicationAdapter : IApplicationAdapter
    {
        public ApplicationAdapter(ILoodsmanApplication application)
        {
            Application = application;
        }

        public ILoodsmanApplication Application { get; }

        public void OpenObjectsInNewWindow(string objectIds, string checkoutName = null)
        {
            if (objectIds == null) return;
            var msgParams = new MsgParams
            {
                Reserved = null,
                CheckoutName = checkoutName,
                ObjectId = 0,
                ObjectsId = objectIds
            };
            var msgParamsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(msgParams));
            Marshal.StructureToPtr(msgParams, msgParamsPtr, false);
            SendMessage((IntPtr)Application.MainHandle, 0x400 + 101, msgParamsPtr, IntPtr.Zero);
            Marshal.FreeHGlobal(msgParamsPtr);
        }

        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private struct MsgParams
        {
            public string Reserved { get; set; }
            public string CheckoutName { get; set; }
            public int ObjectId { get; set; }
            public string ObjectsId { get; set; }
        }
    }
}