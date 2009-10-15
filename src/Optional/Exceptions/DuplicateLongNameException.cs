using System;

namespace Optional.Exceptions
{
	public class DuplicateLongNameException : Exception
	{
		private readonly string longName;

		public DuplicateLongNameException(string longName)
		{
			this.longName = longName;
		}

		public string LongName
		{
			get { return longName; }
		}
	}
}