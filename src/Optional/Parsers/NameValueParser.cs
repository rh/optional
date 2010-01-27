using System.Collections.Generic;

namespace Optional.Parsers
{
    /// <summary>
    /// Parses command-line options into a list of <see cref="Option"/>s of which only the
    /// <see cref="Option.ShortName"/>, <see cref="Option.LongName"/> or <see cref="Option.Value"/>
    /// are set.
    /// </summary>
    /// <example>
    /// A command-line such as <c>foo --bar -x y</c> will lead to a list of 3 options:
    /// <list type="square">
    /// <item>ShortName = "", LongName = "", Value = "foo"</item>
    /// <item>ShortName = "", LongName = "bar", Value = ""</item>
    /// <item>ShortName = "x", LongName = "", Value = "y"</item>
    /// </list>
    /// </example>
    public class NameValueParser
    {
        /// <summary>
        /// Creates a list of options with only names and values set.
        /// </summary>
        /// <param name="args">The command-line arguments of the application.</param>
        public IList<Option> Parse(string[] args)
        {
            IList<Option> options = new List<Option>();
            var option = new Option();

            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (Parser.ShortOption.IsMatch(arg))
                {
                    if (option.ShortName != string.Empty || option.LongName != string.Empty)
                    {
                        option = new Option();
                    }

                    option.ShortName = arg.Substring(1);
                    options.Add(option);
                }
                else if (Parser.LongOption.IsMatch(arg))
                {
                    if (option.ShortName != string.Empty || option.LongName != string.Empty)
                    {
                        option = new Option();
                    }

                    option.LongName = arg.Substring(2);
                    options.Add(option);
                }
                else
                {
                    option.Value = arg;
                    if (!options.Contains(option))
                    {
                        options.Add(option);
                    }
                    option = new Option();
                }
            }

            return options;
        }
    }
}