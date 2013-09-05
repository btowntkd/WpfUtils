using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.Services
{
    public class LowLevelMouseEventArgs : EventArgs
    {


        public LowLevelMouseEventArgs()
        {
        }
    }

    public class MouseHookService : IDisposable
    {
        #region Native Interop

        //Low Level Keyboard Hook Flag
        private const int WH_MOUSE_LL = 14;

        //Keyboard Event Codes
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_MOUSEHWHEEL = 0x020E;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            Int32 x;
            Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public UInt32 mouseData;
            public UInt32 flags;
            public UInt32 time;
            public IntPtr dwExtraInfo;
        }

        #endregion

        #region Private Fields

        NativeHookSubscription hookSubscription = new NativeHookSubscription(WH_MOUSE_LL);

        #endregion

        #region Constructor / IDispose

        public MouseHookService()
        {
            hookSubscription.Hook += HookSubscription_Hook;
        }

        public void Dispose()
        {
            hookSubscription.Hook -= HookSubscription_Hook;
            hookSubscription.Dispose();
        }

        #endregion

        #region Public Events

        public event EventHandler<LowLevelMouseEventArgs> MouseMove = null;
        public event EventHandler<LowLevelMouseEventArgs> MouseWheelScroll = null;
        public event EventHandler<LowLevelMouseEventArgs> MouseButtonDown = null;
        public event EventHandler<LowLevelMouseEventArgs> MouseButtonUp = null;

        #endregion

        #region Protected Methods

        protected void RaiseEvent(EventHandler<LowLevelMouseEventArgs> handler, LowLevelMouseEventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Event Handlers

        private void HookSubscription_Hook(object sender, NativeHookEventArgs args)
        {
            int eventCode = Marshal.ReadInt32(args.WParam);
            var mouseHookInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(args.LParam, typeof(MSLLHOOKSTRUCT));
            var eventArgs = new LowLevelMouseEventArgs();

            switch (eventCode)
            {
                case WM_MOUSEMOVE:
                    RaiseEvent(MouseMove, eventArgs);
                    break;
                case WM_LBUTTONDOWN:
                    RaiseEvent(MouseButtonDown, eventArgs);
                    break;
                case WM_LBUTTONUP:
                    RaiseEvent(MouseButtonUp, eventArgs);
                    break;
                case WM_RBUTTONDOWN:
                    RaiseEvent(MouseButtonDown, eventArgs);
                    break;
                case WM_RBUTTONUP:
                    RaiseEvent(MouseButtonUp, eventArgs);
                    break;
                case WM_MOUSEWHEEL:
                    RaiseEvent(MouseWheelScroll, eventArgs);
                    break;
                case WM_MOUSEHWHEEL:
                    RaiseEvent(MouseWheelScroll, eventArgs);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        #endregion
    }
}
