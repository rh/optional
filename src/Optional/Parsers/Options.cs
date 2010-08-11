using System.Collections.Generic;
using System.Reflection;
using Optional.Attributes;

namespace Optional.Parsers
{
    public static class Options
    {
        public static IList<Option> ToOptions(this string[] args)
        {
            var options = new List<Option>();

            var parser = new NameValueParser
                             {
                                 OnShortOption = name => options.Add(new Option {ShortName = name}),
                                 OnLongOption = name => options.Add(new Option {LongName = name}),
                                 OnValue = value =>
                                               {
                                                   if (options.Count > 0)
                                                   {
                                                       var last = options[options.Count - 1];
                                                       if (last.Value == string.Empty)
                                                       {
                                                           last.Value = value;
                                                           return;
                                                       }
                                                   }

                                                   options.Add(new Option {Value = value});
                                               }
                             };
            parser.Parse(args);

            return options;
        }

        public static IList<Option> Create(object obj)
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

        private static PropertyInfo[] PropertiesOf(object obj)
        {
            const BindingFlags Binding = BindingFlags.Public | BindingFlags.Instance;
            return obj.GetType().GetProperties(Binding);
        }
    }
}