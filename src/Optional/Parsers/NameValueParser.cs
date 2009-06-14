using System.Collections.Generic;
using Optional.Exceptions;

namespace Optional.Parsers
{
    /// <summary>
    /// Parses command-line options into name/value pairs.
    /// </summary>
    public class NameValueParser
    {
        /// <summary>
        /// Creates a dictionary with name/value pairs from the supplied command-line arguments.
        /// </summary>
        /// <param name="args">The command-line arguments of the application.</param>
        public IDictionary<string, string> Parse(string[] args)
        {
            IDictionary<string, string> options = new Dictionary<string, string>();
            string name = null;

            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (Parser.ShortOption.IsMatch(arg))
                {
                    name = arg.Substring(1);
                    options[name] = null;
                }
                else if (Parser.LongOption.IsMatch(arg))
                {
                    name = arg.Substring(2);
                    options[name] = null;
                }
                else
                {
                    if (name == null)
                    {
                        throw new MissingOptionException(arg);
                    }
                    options[name] = arg;
                    name = null;
                }
            }
            return options;
        }
    }
}