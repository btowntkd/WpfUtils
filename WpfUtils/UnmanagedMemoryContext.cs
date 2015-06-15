using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils
{
    /// <summary>
    /// Provides a disposable, managed context for allocating memory, eliminating the need
    /// to use <see cref="Marshal.AllocHGlobal"/> and <see cref="Marshal.FreeHGlobal"/>.
    /// </summary>
    public class UnmanagedMemoryContext : IDisposable
    {
        private readonly object _marshalledDataLock = new object();
        private readonly Dictionary<IntPtr, int> _marshalledData = new Dictionary<IntPtr,int>();

        public UnmanagedMemoryContext()
        { }

        ~UnmanagedMemoryContext()
        {
            FreeAll();
        }

        void IDisposable.Dispose()
        {
            FreeAll();
            GC.SuppressFinalize(this);
        }

        public int TotalAllocated
        {
            get 
            { 
                lock(_marshalledDataLock)
                {
                    return _marshalledData.Values.Sum(); 
                }
            }
        }

        public IntPtr Allocate(int sizeInBytes)
        {
            lock (_marshalledDataLock)
            {
                var ptr = Marshal.AllocHGlobal(sizeInBytes);
                _marshalledData.Add(ptr, sizeInBytes);
                return ptr;
            }
        }

        public void FreeAll()
        {
            lock (_marshalledDataLock)
            {
                foreach (var ptr in _marshalledData.Keys)
                {
                    Marshal.FreeHGlobal(ptr);
                }
                _marshalledData.Clear();
            }
        }

        public void Free(IntPtr ptr)
        {
            lock (_marshalledDataLock)
            {
                if (_marshalledData.ContainsKey(ptr))
                {
                    Marshal.FreeHGlobal(ptr);
                    _marshalledData.Remove(ptr);
                }
            }
        }
    }
}
