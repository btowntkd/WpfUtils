using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace WpfUtils.Services
{
    internal class NativeHookEventArgs : EventArgs
    {
        public int HookCode { get; private set; }
        public IntPtr WParam { get; private set; }
        public IntPtr LParam { get; private set; }

        public NativeHookEventArgs(int hookCode, IntPtr wParam, IntPtr lParam)
        {
            HookCode = hookCode;
            WParam = wParam;
            LParam = lParam;
        }
    }

    internal class NativeHookSubscription : IDisposable
    {
        #region DLLImports

        private delegate IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookCallback lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #region Private Fields

        IntPtr hookHandle = IntPtr.Zero;
        Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        #endregion

        #region Constructor / Destructor / IDisposable

        /// <summary>
        /// Creates a new instance of the <see cref="NativeHookSubscription"/> class and automatically 
        /// </summary>
        /// <param name="hookId">The type of hook to monitor.</param>
        /// <remarks>See the MSDN documentation about 
        /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644990">SetWindowsHookEx</a>
        /// for a complete list of possible hook types.</remarks>
        public NativeHookSubscription(int hookId)
        {
            using(var currentProcess = Process.GetCurrentProcess())
            using(var currentModule = currentProcess.MainModule)
            {
                hookHandle = SetWindowsHookEx(
                    hookId,
                    ProcessHookCallback, 
                    GetModuleHandle(currentModule.ModuleName),
                    0);
            }
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are.
        ~NativeHookSubscription()
        {
            //Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Dispose this object by unregistering it from the Windows Hook procedure.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) 
            {
                // free managed Disposable resources here
            }

            // free native resources here
            UnhookWindowsHookEx(hookHandle);
        }

        #endregion

        #region Public Events

        public EventHandler<NativeHookEventArgs> Hook = null;

        #endregion

        #region Private Methods

        private IntPtr ProcessHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if(nCode >= 0)
                RaiseHookEvent(nCode, wParam, lParam);

            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private void RaiseHookEvent(int hookCode, IntPtr wParam, IntPtr lParam)
        {
            var handler = Hook;
            if (handler != null)
            {
                dispatcher.BeginInvoke(
                    handler,
                    this, new NativeHookEventArgs(hookCode, wParam, lParam));
            }
        }

        #endregion
    }
}
