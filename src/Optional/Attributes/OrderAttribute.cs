using System;

namespace Optional.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class OrderAttribute : Attribute
	{
		private readonly int order;

		public OrderAttribute(int order)
		{
			this.order = order;
		}

		public int Order
		{
			get { return order; }
		}
	}
}