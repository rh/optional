using System;

namespace Optional.Exceptions
{
	public class MissingOptionException : Exception
	{
		private readonly string value;

		public MissingOptionException(string value)
		{
			this.value = value;
		}

		public string Value
		{
			get { return value; }
		}

		public override string ToString()
		{
			return String.Format("No option given for value '{0}'.", Value);
		}
	}
}