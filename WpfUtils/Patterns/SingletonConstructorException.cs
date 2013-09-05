using System;
using System.Runtime.Serialization;

namespace WpfUtils.Patterns
{
    /// <summary>
    /// Represents constructor errors that occur while creating a singleton.
    /// </summary>
    public class SingletonConstructorException : Exception, ISerializable
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public SingletonConstructorException()
        {
        }

        /// <summary>
        /// Initializes a new instance with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SingletonConstructorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance with a reference to the inner 
        /// exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified.
        /// </param>
        public SingletonConstructorException(Exception innerException)
            : base(null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance with a specified error message and a 
        /// reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified.
        /// </param>
        public SingletonConstructorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the 
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains 
        /// contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="SerializationException">The class name is null or System.Exception.HResult is zero (0).</exception>
        protected SingletonConstructorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
