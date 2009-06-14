using System.IO;

namespace Optional
{
    public interface IApplicationContext
    {
        /// <summary>The standard output stream.</summary>
        TextWriter Out { get; }

        /// <summary>The standard debug stream.</summary>
        TextWriter Debug { get; }

        /// <summary>The arguments as they were supplied to the application's Main method.</summary>
        string[] ApplicationArguments { get; }

        string ApplicationName { get; }

        string ApplicationVersion { get; }

        string ApplicationDescription { get; }

        string ApplicationCopyright { get; }

        /// <summary>The directory in which the application is located.</summary>
        string ApplicationDirectory { get; }

        string ApplicationDataDirectory { get; }

        string CurrentDirectory { get; }
    }
}