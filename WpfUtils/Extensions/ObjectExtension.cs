using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="Object"/> class.
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Attempt to cast the object to the given type, using the "as" keyword.
        /// </summary>
        /// <typeparam name="T">The type into which the object instance should be cast.</typeparam>
        /// <param name="instance">The target object instance.</param>
        /// <returns>Returns the object, cast to the target type.  If the object could not be cast, returns null.</returns>
        public static T As<T>(this Object instance)
            where T : class
        {
            return instance as T;
        }

        /// <summary>
        /// Reflect over the object's type, converting the object into a Dictionary,
        /// containing each property name and value as a key/value pair.
        /// </summary>
        /// <param name="instance">The target object over which to reflect.</param>
        /// <returns>Returns the Dictionary of property names and values.</returns>
        public static Dictionary<string, object> ToDictionary(this Object instance)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
            if (instance != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(instance))
                {
                    object value = descriptor.GetValue(instance);
                    dictionary.Add(descriptor.Name, value);
                }
            }
            return dictionary;
        }
    }
}
