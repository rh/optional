using System;

namespace Optional.Exceptions
{
	public class DuplicateOptionException : Exception
	{
		private readonly Option option;

		public DuplicateOptionException(Option option)
		{
			this.option = option;
		}

		public Option Option
		{
			get { return option; }
		}

		public override string ToString()
		{
			return string.Format("Option '{0}' was already set.", Option.LongName);
		}
	}
}