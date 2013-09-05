using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace WpfUtils.Services
{
    public class LowLevelKeyEventArgs : EventArgs
    {
        public Key Key { get; private set; }

        public LowLevelKeyEventArgs(Key key)
        {
            this.Key = key;
        }
    }

    public class KeyboardHookService : IDisposable
    {
        #region Native Interop

        //Low Level Keyboard Hook Flag
        private const int WH_KEYBOARD_LL = 13;

        //Keyboard Event Codes
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public UInt32 vkCode;
            public UInt32 scanCode;
            public UInt32 flags;
            public UInt32 time;
            public IntPtr dwExtraInfo;
        }

        #endregion

        #region Private Fields

        NativeHookSubscription hookSubscription = new NativeHookSubscription(WH_KEYBOARD_LL);

        #endregion

        #region Constructor / IDispose

        public KeyboardHookService()
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

        public event EventHandler<LowLevelKeyEventArgs> KeyDown = null;
        public event EventHandler<LowLevelKeyEventArgs> KeyUp = null;

        #endregion

        #region Protected Methods

        protected void RaiseEvent(EventHandler<LowLevelKeyEventArgs> handler, LowLevelKeyEventArgs args)
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
            var keyboardHookInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(args.LParam, typeof(KBDLLHOOKSTRUCT));
            var eventArgs = new LowLevelKeyEventArgs(
                KeyInterop.KeyFromVirtualKey((int)keyboardHookInfo.vkCode));

            switch (eventCode)
            {
                case WM_KEYDOWN:
                    RaiseEvent(KeyDown, eventArgs);
                    break;
                case WM_KEYUP:
                    RaiseEvent(KeyUp, eventArgs);
                    break;
                case WM_SYSKEYDOWN:
                    RaiseEvent(KeyDown, eventArgs);
                    break;
                case WM_SYSKEYUP:
                    RaiseEvent(KeyUp, eventArgs);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        #endregion
    }
}
