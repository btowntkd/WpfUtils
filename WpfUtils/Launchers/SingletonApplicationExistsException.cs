using System;
using System.Runtime.Serialization;

namespace WpfUtils.Launchers
{
    /// <summary>
    /// Exception thrown when the <see cref="SingletonApplicationLauncher"/> determines that an application instance
    /// already exists.  Catch this exception to handle special logic like manually assigning focus to the external instance.
    /// </summary>
    public class SingletonApplicationExistsException : Exception, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonApplicationExistsException" /> class.
        /// </summary>
        public SingletonApplicationExistsException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonApplicationExistsException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SingletonApplicationExistsException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonApplicationExistsException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SingletonApplicationExistsException(string message, SingletonApplicationExistsException innerException)
            : base(message, innerException)
        { }


        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonApplicationExistsException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected SingletonApplicationExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}