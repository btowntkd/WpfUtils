using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtils.Patterns
{
    /// <summary>
    /// Interface used for "popsicle" immutable objects.
    /// Allows an object to be edited and then "Frozen,"
    /// making it effectively immutable from that point forward.
    /// </summary>
    /// <remarks>
    /// Combine with <see cref="ICloneable"/> or similar deep-copy implementation,
    /// in order to create non-Frozen copies of a Frozen object.
    /// 
    /// An object which has been frozen should throw a
    /// <see cref="FrozenObjectException"/> on any attempt to change one of
    /// its properties.
    /// </remarks>
    public interface IFreezable
    {
        /// <summary>
        /// Get the value indicating whether or not the object is "Frozen."
        /// </summary>
        bool IsFrozen { get; }

        /// <summary>
        /// Freeze the object, making it immutable/read-only for the remainder of its life.
        /// </summary>
        void Freeze();
    }
}
