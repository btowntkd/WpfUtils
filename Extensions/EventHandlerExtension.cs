using System;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Adds Extension Methods to the <see cref="EventHandler"/> 
    /// and <see cref="EventHandler{T}"/> classes.
    /// </summary>
    public static class EventHandlerExtension
    {
        #region EventHandler Extensions

        /// <summary>
        /// Raises the Event, safely checking for a null value first.
        /// </summary>
        /// <param name="handler">The current EventHandler.</param>
        /// <param name="sender">The object in which the event was raised.</param>
        /// <param name="args">The event args.</param>
        public static void Raise(this EventHandler handler, object sender, EventArgs args)
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        #endregion

        #region EventHandler{T} Extensions

        /// <summary>
        /// Raises the Event, safely checking for a null value first.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of Event args which should be passed.</typeparam>
        /// <param name="handler">The current EventHandler.</param>
        /// <param name="sender">The object in which the event was raised.</param>
        /// <param name="args">The event args.</param>
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs args) where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        #endregion
    }
}
