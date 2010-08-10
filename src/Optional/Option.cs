using System;
using System.Reflection;
using System.Text;
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

        public Option()
        {
            ShortName = string.Empty;
            LongName = string.Empty;
            Description = string.Empty;
            Required = false;
            Value = string.Empty;
        }

        public Option(PropertyInfo property)
        {
            ShortName = ShortNameOf(property);
            LongName = LongNameOf(property);
            Description = DescriptionOf(property);
            Required = IsRequired(property);
            Value = string.Empty;
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
            var output = new StringBuilder();


            if (!String.IsNullOrEmpty(ShortName))
            {
                output.AppendFormat("-{0} ", ShortName);
            }

            if (!String.IsNullOrEmpty(LongName))
            {
                output.AppendFormat("--{0} ", LongName);
            }

            if (!String.IsNullOrEmpty(Value))
            {
                output.AppendFormat("{0}", Value);
            }

            return output.ToString();
        }
    }
}