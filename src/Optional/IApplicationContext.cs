using System.Collections.Generic;
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
        string[] Arguments { get; }

        /// <summary>The options as they are parsed from <see cref="Arguments"/>.</summary>
        IList<Option> Options { get; set; }

        /// <summary>The name of the application.</summary>
        string Name { get; }

        /// <summary>The version of the application.</summary>
        string Version { get; }

        /// <summary>A description of the application.</summary>
        string Description { get; }

        /// <summary>The copyright notice of the application.</summary>
        string Copyright { get; }

        /// <summary>The directory in which the application is located.</summary>
        string Location { get; }
    }
}