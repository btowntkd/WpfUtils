using System;
using System.Reflection;

namespace WpfUtils.Patterns
{
    /// <summary>
    /// Static factory class for implementing the singleton pattern on Types
    /// which contain a private, parameter-less constructor.
    /// </summary>
    /// <typeparam name="T">The underlying singleton type.</typeparam>
    public static class Singleton<T> where T : class
    {
        private static T _instance = null;
        private static readonly object _creationLock = new object();

        /// <summary>
        /// Default static constructor prevents the Type from being marked with beforefieldinit.
        /// </summary>
        /// <remarks>
        /// This simply makes sure that static fields initialization occurs 
        /// when the Instance property is called the first time, and not before.
        /// </remarks>
        static Singleton()
        { }

        /// <summary>
        /// Get the singleton instance for the class.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_creationLock)
                    {
                        //Use the Double-check lock pattern
                        if (_instance == null)
                        {
                            ConstructorInfo constructor = null;
                            try
                            {
                                // Binding flags include private constructors.
                                constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                                _instance = (T)constructor.Invoke(null);
                            }
                            catch (Exception exception)
                            {
                                throw new SingletonConstructorException(exception);
                            }
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
