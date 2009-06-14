using System.Collections.Generic;
using System.Reflection;
using Optional.Attributes;

namespace Optional.Parsers
{
    public class OptionParser
    {
        public IList<Option> Parse(object obj)
        {
            var options = new List<Option>();
            foreach (var property in PropertiesOf(obj))
            {
                if (!IgnoreAttribute.IsDefinedOn(property))
                {
                    options.Add(new Option(property));
                }
            }
            return options;
        }

        public static PropertyInfo[] PropertiesOf(object obj)
        {
            const BindingFlags Binding = BindingFlags.Public | BindingFlags.Instance;
            return obj.GetType().GetProperties(Binding);
        }
    }
}