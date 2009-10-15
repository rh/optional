using System;

namespace Optional.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class RequiredAttribute : Attribute
	{
	}
}