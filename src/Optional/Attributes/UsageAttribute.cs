using System;

namespace Optional.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class UsageAttribute : ValueAttribute
	{
		public UsageAttribute(string value)
			: base(value)
		{
		}
	}
}