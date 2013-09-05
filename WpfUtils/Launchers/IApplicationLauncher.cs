using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtils.Launchers
{
    /// <summary>
    /// Provides a common interface for launching an application.
    /// </summary>
    public interface IApplicationLauncher
    {
        /// <summary>
        /// Launch the associated application.
        /// </summary>
        void Launch();
    }
}
