using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Optional.Extensions;
using Optional.Parsers;

namespace Optional
{
    /// <summary>
    /// The default implementation of <see cref="IApplicationContext"/>.
    /// </summary>
    public class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(string[] args)
        {
            Arguments = args;

            Options = args.ToOptions();

            var assembly = Assembly.GetEntryAssembly();

            Name = assembly.GetName().Name;

            Version = assembly.Version();

            Description = assembly.Description();

            Copyright = assembly.Copyright();

            Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Out = Debug = Console.Out;
        }

        public string[] Arguments { get; set; }

        public IList<Option> Options { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public string Copyright { get; set; }

        public string Location { get; set; }

        public TextWriter Out { get; set; }

        public TextWriter Debug { get; set; }
    }
}