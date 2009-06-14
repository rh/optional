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
            ApplicationArguments = args;

            var assembly = Assembly.GetEntryAssembly();
            ApplicationName = assembly.GetName().Name;

            var version = assembly.GetName().Version;
            ApplicationVersion = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            ApplicationDescription = string.Empty;
            if (assembly.IsDefined(typeof(AssemblyDescriptionAttribute), false))
            {
                var attribute = (AssemblyDescriptionAttribute) assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
                ApplicationDescription = attribute.Description;
            }

            ApplicationCopyright = string.Empty;
            if (assembly.IsDefined(typeof(AssemblyCopyrightAttribute), false))
            {
                var attribute = (AssemblyCopyrightAttribute) assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
                ApplicationCopyright = attribute.Copyright;
            }

            ApplicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ApplicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);

            CurrentDirectory = Environment.CurrentDirectory;

            Out = Debug = Console.Out;
        }

        public string[] ApplicationArguments { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string ApplicationDescription { get; set; }
        public string ApplicationCopyright { get; set; }
        public string ApplicationDirectory { get; set; }
        public string ApplicationDataDirectory { get; set; }
        public string CurrentDirectory { get; set; }
        public TextWriter Out { get; set; }
        public TextWriter Debug { get; set; }
    }
}