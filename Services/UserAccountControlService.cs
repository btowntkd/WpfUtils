using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfUtils.Services
{
    /// <summary>
    /// A common interface for detecting Administrative privileges
    /// and relaunching an application with the Admin token.
    /// </summary>
    public interface IUserAccountControlService
    {
        /// <summary>
        /// Get the value indicating whether or not the current process
        /// is currently running with the Administrator token.
        /// </summary>
        bool IsAdmin { get; }

        /// <summary>
        /// Shutdown the application and immediately re-launch it with
        /// the Administrator token requested.
        /// </summary>
        void ShutdownAndRelaunchAsAdmin();
    }

    /// <summary>
    /// Implementation of the <see cref="IUserAccountControlService"/> interface
    /// for WPF applications
    /// </summary>
    public class UserAccountControlService : IUserAccountControlService
    {
        #region Private Fields

        /// <summary>
        /// A reference to the currently-running <see cref="Application"/>.
        /// </summary>
        private Application _application;

        /// <summary>
        /// Internal flag used to track whether or not we should re-launch
        /// the current process with the Administrator token,
        /// once the Application is shut down.
        /// </summary>
        private bool _relaunchAsAdmin;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the <see cref="UserAccountControlService"/>.
        /// </summary>
        /// <param name="application">
        /// An instance of the current <see cref="Application"/> object.
        /// The service will use this to perform shutdown and re-launch operations.
        /// </param>
        public UserAccountControlService(Application application)
        {
            if (application == null)
                throw new ArgumentNullException("application");

            Application = application;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get the value indicating whether the current process 
        /// is running with the Administrator token.
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                WindowsPrincipal wp = new WindowsPrincipal(wi);
                return wp.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// Shutdown the application and immediately re-launch it with
        /// the Administrator token requested.
        /// </summary>
        public void ShutdownAndRelaunchAsAdmin()
        {
            //Set the internal "re-launch as admin" flag
            _relaunchAsAdmin = true;

            //Trigger an application shutdown
            Application.Shutdown();
        }

        #endregion

        #region Protected Methods / Properties

        /// <summary>
        /// Get or Set the internally-stored instance of the
        /// currently-running <see cref="Application"/>.
        /// </summary>
        protected Application Application
        {
            get { return _application; }
            set
            {
                if (_application != value)
                {
                    //Detach event handlers from the previous instance
                    UnregisterApplicationListeners(_application);

                    _application = value;

                    //Attach event handlers to the new instance
                    RegisterApplicationListeners(_application);
                }
            }
        }

        /// <summary>
        /// Launch another instance of the currently-running process,
        /// requesting the Administrator token.
        /// </summary>
        protected void RelaunchCurrentProcessAsAdministrator()
        {
            //Gather the Executable path and command-line arguments
            //which were used to originally launch the Application
            var exeName = Process.GetCurrentProcess().MainModule.FileName;
            var args = Process.GetCurrentProcess().StartInfo.Arguments;

            //Start a new process using the same path and args,
            //using the "runas" verb to request the Administrator token
            ProcessStartInfo startInfo = new ProcessStartInfo(exeName, args);
            startInfo.Verb = "runas";
            startInfo.UseShellExecute = true;

            //Launch the new process
            Process.Start(startInfo);
        }

        /// <summary>
        /// Attach event handlers to the given <see cref="Application"/> instance.
        /// </summary>
        /// <param name="app">
        /// The <see cref="Application"/> instance for which to attach event handlers.
        /// </param>
        protected void RegisterApplicationListeners(Application app)
        {
            if (app != null)
            {
                app.Exit += new ExitEventHandler(Application_Exit);
            }
        }

        /// <summary>
        /// Detach event handlers from the given <see cref="Application"/> instance.
        /// </summary>
        /// <param name="app">
        /// The <see cref="Application"/> instance for which to detach any event handlers.
        /// </param>
        protected void UnregisterApplicationListeners(Application app)
        {
            if (app != null)
            {
                app.Exit -= new ExitEventHandler(Application_Exit);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the event raised when the stored <see cref="Application"/>
        /// instance is exiting.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //If the "relaunch as admin" flag has been set, 
            //then we likely triggered the shutdown from within this service,
            //and we should now relaunch the process with the Administrator token.
            if (_relaunchAsAdmin)
            {
                RelaunchCurrentProcessAsAdministrator();
            }
        }

        #endregion
    }
}
