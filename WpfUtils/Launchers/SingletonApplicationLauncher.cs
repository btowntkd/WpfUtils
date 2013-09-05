using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WpfUtils.Launchers
{
    /// <summary>
    /// Provides a method for launching only a single application instance.
    /// If an instance already exists, the class will throw a <see cref="SingletonApplicationExistsException"/>.
    /// Catch this exception to handle special logic like passing focus to the existing instance, or
    /// displaying an error message.
    /// </summary>
    public class SingletonApplicationLauncher : IApplicationLauncher, IDisposable
    {
        #region Private Const Fields

        private const string LocalPrefix = @"Local\";
        private const string GlobalPrefix = @"Global\";

        #endregion

        #region Private Fields

        private IApplicationLauncher _newInstanceLauncher = null;
        private readonly string _applicationId;
        private bool _global = false;
        private Mutex _applicationInstanceMutex = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the <see cref="SingletonApplicationLauncher"/>.
        /// </summary>
        /// <param name="internalLancher">The <see cref="IApplicationLauncher"/>
        /// to use internally, when launching a new instance of the application.</param>
        /// <param name="applicationID">The unique string used to identify the single-instance application.
        /// Typically this value is a GUID, although any non-empty string is allowed.</param>
        /// <param name="global">Determines whether to limit the single instance Globally or Locally.
        /// 
        /// If true, the single instance of the application will be limited across all
        /// user accounts on the current machine.
        /// 
        /// If false, a single instance of the application will be allowed
        /// per user account on the machine.</param>
        public SingletonApplicationLauncher(IApplicationLauncher internalLancher, string applicationID, bool global = false)
        {
            if (internalLancher == null)
            {
                throw new ArgumentNullException("internalLauncher", "Internal Application Launcher cannot be null");
            }
            if (string.IsNullOrWhiteSpace(applicationID))
            {
                throw new ArgumentNullException("applicationID", "Application Identifier cannot be null or empty.");
            }

            _newInstanceLauncher = internalLancher;
            _applicationId = applicationID;
            _global = global;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or Set the <see cref="IApplicationLauncher"/> to invoke
        /// if another instance of the application could not be found.
        /// </summary>
        public IApplicationLauncher InternalLauncher
        {
            get { return _newInstanceLauncher; }
        }

        /// <summary>
        /// Get the Unique Application ID used to identify the single-instance application.
        /// Typically this value is a GUID, although any non-empty string is allowed.
        /// </summary>
        public string ApplicationId
        {
            get { return _applicationId; }
        }

        /// <summary>
        /// Get the value indicating whether or not to include all application
        /// instances across all desktop sessions.
        /// </summary>
        /// <remarks>
        /// If true, the single instance of the application will be limited across all
        /// user accounts on the current machine.
        /// 
        /// If false, a single instance of the application will be allowed
        /// per user account on the machine.
        /// </remarks>
        public bool Global
        {
            get { return _global; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Attempt to launch the application.  If a single instance of the application already exists,
        /// the method will throw an <see cref="SingletonApplicationExistsException"/>.
        /// </summary>
        /// <exception cref="SingletonApplicationExistsException">
        /// If an application instance already exists, the method will throw a <see cref="SingletonApplicationExistsException"/>.
        /// The calling method must take the necessary actions to respond to this scenario,
        /// (for example: by displaying a dialog box or bringing the existing application to the front).
        /// </exception>
        public void Launch()
        {
            bool createNewInstance = false;
            string mutexId = CreateMutexIdentifier();
            _applicationInstanceMutex = new Mutex(true, mutexId, out createNewInstance);

            if (createNewInstance)
            {
                if (InternalLauncher != null)
                    InternalLauncher.Launch();
                GC.KeepAlive(_applicationInstanceMutex);
            }
            else
            {
                throw new SingletonApplicationExistsException();
            }
        }

        /// <summary>
        /// Dispose of the SingleInstance launcher, releasing the common resource Mutex.
        /// </summary>
        void IDisposable.Dispose()
        {
            if (_applicationInstanceMutex != null)
            {
                _applicationInstanceMutex.Close();
            }
        }

        #endregion

        #region Private Methods

        private string CreateMutexIdentifier()
        {
            return string.Format("{0}{1}",
                Global ? GlobalPrefix : LocalPrefix,
                ApplicationId);
        }

        #endregion
    }
}
