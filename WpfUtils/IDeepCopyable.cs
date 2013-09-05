
namespace WpfUtils
{
    /// <summary>
    /// Provides an interface through which to create deep copies of objects.
    /// </summary>
    /// <typeparam name="T">The type of the object supporting deep-copy functionality.</typeparam>
    public interface IDeepCopyable<T>
        where T : IDeepCopyable<T>
    {
        /// <summary>
        /// Perform a deep copy of the object, returning the cloned instance.
        /// </summary>
        /// <returns>Returns a deeply-cloned instance of the current object.</returns>
        T DeepCopy();
    }
}
