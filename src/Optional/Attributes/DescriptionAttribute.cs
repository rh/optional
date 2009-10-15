using System;

namespace Optional.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class DescriptionAttribute : ValueAttribute
	{
		public DescriptionAttribute(string value)
			: base(value)
		{
		}
	}
}