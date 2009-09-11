using System;
using System.IO;
using System.Reflection;

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

            var assembly = Assembly.GetEntryAssembly();
            Name = assembly.GetName().Name;

            var version = assembly.GetName().Version;
            Version = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            Description = string.Empty;
            if (assembly.IsDefined(typeof(AssemblyDescriptionAttribute), false))
            {
                var attribute = (AssemblyDescriptionAttribute) assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
                Description = attribute.Description;
            }

            Copyright = string.Empty;
            if (assembly.IsDefined(typeof(AssemblyCopyrightAttribute), false))
            {
                var attribute = (AssemblyCopyrightAttribute) assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
                Copyright = attribute.Copyright;
            }

            Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Out = Debug = Console.Out;
        }

        public string[] Arguments { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Copyright { get; set; }
        public string Location { get; set; }
        public TextWriter Out { get; set; }
        public TextWriter Debug { get; set; }
    }
}