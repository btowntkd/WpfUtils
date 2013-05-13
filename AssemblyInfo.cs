using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WpfUtils
{
    /// <summary>
    /// Reads assembly information from a target Assembly object.
    /// </summary>
    public class AssemblyInfo
    {
        #region Private Data

        Assembly _targetAssembly = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="AssemblyInfo"/> instance, using the process executable as the Target Assembly.
        /// </summary>
        public AssemblyInfo()
        {
            _targetAssembly = Assembly.GetEntryAssembly();
        }

        /// <summary>
        /// Creates a new <see cref="AssemblyInfo"/> instance, with the given Assembly object used as the Target Assembly.
        /// </summary>
        /// <param name="targetAssembly">The assembly to use when reading assembly attributes.</param>
        public AssemblyInfo(Assembly targetAssembly)
        {
            _targetAssembly = targetAssembly;
        }

        /// <summary>
        /// Creates a new <see cref="AssemblyInfo"/> instance, with the given filename used to load the Target Assembly.
        /// </summary>
        /// <param name="assemblyFilePath">The file path of the Assembly to read.</param>
        public AssemblyInfo(string assemblyFilePath)
        {
            _targetAssembly = Assembly.LoadFile(assemblyFilePath);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get the associated <see cref="Assembly"/> object.
        /// </summary>
        public Assembly Assembly
        {
            get { return _targetAssembly; }
        }

        /// <summary>
        /// Get the string value specifying the Title (or Name) of the assembly.
        /// </summary>
        public string Title
        {
            get { return GetAssemblyAttribute<AssemblyTitleAttribute>(new AssemblyTitleAttribute(string.Empty)).Title; }
        }

        /// <summary>
        /// Get the string value specifying a short description that summarizes the nature and purpose of the assembly.
        /// </summary>
        public string Description
        {
            get { return GetAssemblyAttribute<AssemblyDescriptionAttribute>(new AssemblyDescriptionAttribute(string.Empty)).Description; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's Company name.
        /// </summary>
        public string Company
        {
            get { return GetAssemblyAttribute<AssemblyCompanyAttribute>(new AssemblyCompanyAttribute(string.Empty)).Company; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's Product information.
        /// </summary>
        public string Product
        {
            get { return GetAssemblyAttribute<AssemblyProductAttribute>(new AssemblyProductAttribute(string.Empty)).Product; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's Copyright information.
        /// </summary>
        public string Copyright
        {
            get { return GetAssemblyAttribute<AssemblyCopyrightAttribute>(new AssemblyCopyrightAttribute(string.Empty)).Copyright; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's Trademark information.
        /// </summary>
        public string Trademark
        {
            get { return GetAssemblyAttribute<AssemblyTrademarkAttribute>(new AssemblyTrademarkAttribute(string.Empty)).Trademark; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's version
        /// (usually in the format "major.minor.build.revision").
        /// </summary>
        public Version Version
        {
            get { return Assembly.GetName().Version; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's Win32 file version.
        /// This often defaults to the assembly version.
        /// </summary>
        public string FileVersion
        {
            get { return GetAssemblyAttribute<AssemblyFileVersionAttribute>(new AssemblyFileVersionAttribute(string.Empty)).Version; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's version information that is NOT used by the runtime,
        /// such as a full product version number.
        /// </summary>
        public string InformationalVersion
        {
            get { return GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(new AssemblyInformationalVersionAttribute(string.Empty)).InformationalVersion; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's culture.
        /// </summary>
        public string Culture
        {
            get { return GetAssemblyAttribute<AssemblyCultureAttribute>(new AssemblyCultureAttribute(string.Empty)).Culture; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's flags.
        /// </summary>
        public AssemblyNameFlags Flags
        {
            get { return (AssemblyNameFlags)GetAssemblyAttribute<AssemblyFlagsAttribute>(new AssemblyFlagsAttribute(AssemblyNameFlags.None)).AssemblyFlags; }
        }

        /// <summary>
        /// Get the string value specifying the assembly's configuration (for example, "Debug" or "Release").
        /// The runtime does not use this value.
        /// </summary>
        public string Configuration
        {
            get { return GetAssemblyAttribute<AssemblyConfigurationAttribute>(new AssemblyConfigurationAttribute(string.Empty)).Configuration; }
        }

        /// <summary>
        /// Get the string value specifying a default alias to be used by referencing assemblies. 
        /// This value provides a friendly name when the name of the assembly itself is not friendly
        /// (such as a GUID value). This value can also be used as a short form of the full assembly name.
        /// </summary>
        public string DefaultAlias
        {
            get { return GetAssemblyAttribute<AssemblyDefaultAliasAttribute>(new AssemblyDefaultAliasAttribute(string.Empty)).DefaultAlias; }
        }

        /// <summary>
        /// Get the boolean value indicating that delay signing is being used by the assembly.
        /// </summary>
        public bool DelaySign
        {
            get { return GetAssemblyAttribute<AssemblyDelaySignAttribute>(new AssemblyDelaySignAttribute(false)).DelaySign; }
        }

        /// <summary>
        /// Get the string value indicating the name of the file that contains either the public key
        /// (if using delay signing) or both the public and private keys passed as a parameter to the
        /// constructor of the attribute.
        /// 
        /// Note that the file name is relative to the output file path (the .exe or .dll), not the source file path.
        /// </summary>
        public string KeyFile
        {
            get { return GetAssemblyAttribute<AssemblyKeyFileAttribute>(new AssemblyKeyFileAttribute(string.Empty)).KeyFile; }
        }

        /// <summary>
        /// Get the string value indicating the key container that contains the key pair passed as a
        /// parameter to the constructor of the attribute.
        /// </summary>
        public string KeyName
        {
            get { return GetAssemblyAttribute<AssemblyKeyNameAttribute>(new AssemblyKeyNameAttribute(string.Empty)).KeyName; }
        }

        /// <summary>
        /// Get the GUID of the assembly.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid(GetAssemblyAttribute<GuidAttribute>(new GuidAttribute(Guid.Empty.ToString())).Value); }
        }

        /// <summary>
        /// Get the boolean value representing whether or not the assembly is COM-visible.
        /// </summary>
        public bool ComVisible
        {
            get { return GetAssemblyAttribute<ComVisibleAttribute>(new ComVisibleAttribute(false)).Value; }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Attempt to retrieve the specified Assembly attribute from the target Assembly,
        /// returning the provided default value if the attribute is not found.
        /// </summary>
        /// <typeparam name="AttributeTypeT">The Assembly Attribute type to retrieve.</typeparam>
        /// <param name="defaultValue">The default value to return, if the attribute is not found.</param>
        /// <returns>Returns the specified Assembly Attribute if found, otherwise returns given default value.</returns>
        protected AttributeTypeT GetAssemblyAttribute<AttributeTypeT>(AttributeTypeT defaultValue) where AttributeTypeT : class
        {
            if (Assembly != null)
            {
                object[] attributes = Assembly.GetCustomAttributes(typeof(AttributeTypeT), false);

                if (attributes.Length > 0)
                {
                    return attributes[0] as AttributeTypeT;
                }
            }

            return defaultValue;
        }

        #endregion
    }

    /// <summary>
    /// Extension methods for the <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retrieve <see cref="AssemblyInfo"/> for the current <see cref="Assembly"/> instance.
        /// </summary>
        /// <param name="assembly">The assembly for which to retrieve the <see cref="AssemblyInfo"/>.</param>
        /// <returns>Returns the <see cref="AssemblyInfo"/> for the given assembly.</returns>
        public static AssemblyInfo GetAssemblyInfo(this Assembly assembly)
        {
            return new AssemblyInfo(assembly);
        }
    }
}
