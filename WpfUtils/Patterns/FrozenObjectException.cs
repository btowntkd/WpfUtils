using System;
using System.Runtime.Serialization;

namespace WpfUtils.Patterns
{
    /// <summary>
    /// Exception thrown when an object implementing the <see cref="IFreezable"/>
    /// interface is changed, after it has been "Frozen."
    /// </summary>
    public class FrozenObjectException : Exception, ISerializable
    {
        /// <summary>
        /// Default empty constructor.
        /// </summary>
        public FrozenObjectException()
        {
        }

        /// <summary>
        /// Constructor which provides a short description of the error.
        /// </summary>
        /// <param name="message">A message describing the conditions
        /// surrounding the exception.</param>
        public FrozenObjectException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor which provides a description of the error 
        /// and wraps an inner exception.
        /// </summary>
        /// <param name="message">A message describing the conditions
        /// surrounding the exception.</param>
        /// <param name="innerException">A nested exception which triggered
        /// the current one.</param>
        public FrozenObjectException(string message, FrozenObjectException innerException)
            : base(message, innerException)
        {
        }


        /// <summary>
        /// Constructor provided for serialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected FrozenObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}