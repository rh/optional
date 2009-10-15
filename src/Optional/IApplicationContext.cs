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

		string Name { get; }

		string Version { get; }

		string Description { get; }

		string Copyright { get; }

		/// <summary>The directory in which the application is located.</summary>
		string Location { get; }
	}
}