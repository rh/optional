using System;

namespace Optional.Exceptions
{
	public class RequirementException : Exception
	{
		private readonly Option option;

		public RequirementException(Option option)
		{
			this.option = option;
		}

		public Option Option
		{
			get { return option; }
		}

		public override string ToString()
		{
			return string.Format("Required option '{0}' was not set.", Option.LongName);
		}
	}
}