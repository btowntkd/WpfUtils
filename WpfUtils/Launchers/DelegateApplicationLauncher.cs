using System;

namespace WpfUtils.Launchers
{
    /// <summary>
    /// Provides a method for launching an Application Window through execution of a
    /// custom <see cref="Action"/> delegate.
    /// </summary>
    public class DelegateApplicationLauncher : IApplicationLauncher
    {
        private readonly Action _launchAction;

        /// <summary>
        /// Create a new <see cref="DelegateApplicationLauncher"/> with the given
        /// <see cref="Action"/> delegate.
        /// </summary>
        /// <param name="launchAction">The action to execute when <see cref="Launch"/> is called.</param>
        public DelegateApplicationLauncher(Action launchAction)
        {
            if (launchAction == null)
                throw new ArgumentNullException("launchAction");

            _launchAction = launchAction;
        }

        /// <summary>
        /// Launch the application by executing the delegate.
        /// </summary>
        public void Launch()
        {
            _launchAction();
        }
    }
}
