using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils
{
    /// <summary>
    /// Provides an interface through which to create deep copies of objects.
    /// </summary>
    /// <typeparam name="T">The type of the object being copied.</typeparam>
    public interface IDeepCopyable<T>
    {
        new T DeepCopy();
    }
}
