using System.Reflection;

namespace Optional.Extensions
{
    public static class AssemblyExtensions
    {
        public static string Version(this Assembly assembly)
        {
            var version = assembly.GetName().Version;

            if (version.Minor == 0 && version.Build == 0 && version.Revision == 0)
            {
                return string.Format("{0}.0", version.Major);
            }

            if (version.Build == 0 && version.Revision == 0)
            {
                return string.Format("{0}.{1}.0", version.Major, version.Minor);
            }

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        public static string Description(this Assembly assembly)
        {
            if (assembly.IsDefined(typeof(AssemblyDescriptionAttribute), false))
            {
                var attribute = (AssemblyDescriptionAttribute) assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
                return attribute.Description;
            }

            return string.Empty;
        }

        public static string Copyright(this Assembly assembly)
        {
            if (assembly.IsDefined(typeof(AssemblyCopyrightAttribute), false))
            {
                var attribute = (AssemblyCopyrightAttribute) assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
                return attribute.Copyright;
            }

            return string.Empty;
        }
    }
}