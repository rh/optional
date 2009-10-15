using System;
using System.Reflection;
using Optional.Attributes;

namespace Optional
{
	public class Option
	{
		public string ShortName { get; set; }

		public string LongName { get; set; }

		public string Description { get; set; }

		public bool Required { get; set; }

		/// <summary>The value given on the command-line.</summary>
		public string Value { get; set; }

		public Type Type { get; set; }

		public PropertyInfo Property { get; set; }

		public Option(PropertyInfo property)
		{
			ShortName = ShortNameOf(property);
			LongName = LongNameOf(property);
			Description = DescriptionOf(property);
			Required = IsRequired(property);
			Type = TypeOf(property);
			Property = property;
		}

		private static string ShortNameOf(PropertyInfo property)
		{
			if (property.IsDefined(typeof(ShortNameAttribute), false))
			{
				var attribute = (ShortNameAttribute) property.GetCustomAttributes(typeof(ShortNameAttribute), false)[0];
				return attribute.Value;
			}
			return property.Name.Substring(0, 1).ToLower();
		}

		private static string LongNameOf(PropertyInfo property)
		{
			if (property.IsDefined(typeof(LongNameAttribute), false))
			{
				var attribute = (LongNameAttribute) property.GetCustomAttributes(typeof(LongNameAttribute), false)[0];
				return attribute.Value;
			}
			return property.Name.ToLower();
		}

		private static string DescriptionOf(ICustomAttributeProvider property)
		{
			if (property.IsDefined(typeof(DescriptionAttribute), false))
			{
				var attribute = (DescriptionAttribute) property.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
				return attribute.Value;
			}
			return string.Empty;
		}

		private static bool IsRequired(ICustomAttributeProvider property)
		{
			return property.IsDefined(typeof(RequiredAttribute), false);
		}

		private static Type TypeOf(PropertyInfo property)
		{
			return property.PropertyType;
		}

		public override string ToString()
		{
			if (!String.IsNullOrEmpty(ShortName))
			{
				return ShortName;
			}
			if (!String.IsNullOrEmpty(LongName))
			{
				return LongName;
			}
			return base.ToString();
		}
	}
}