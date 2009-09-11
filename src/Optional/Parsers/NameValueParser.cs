using System;
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
        /// The action which is called when a value without an option is encountered.
        /// The default action is to throw a <see cref="MissingOptionException"/>.
        /// </summary>
        /// <example>
        /// This example shows how the parser can save all values without an option:
        /// <code>
        /// <![CDATA[
        /// var values = new List<string>();
        /// var parser = new NameValueParser();
        /// parser.OnMissingOption = value => values.Add(value);
        /// ]]>
        /// </code>
        /// </example>
        public Action<string> OnMissingOption = value => { throw new MissingOptionException(value); };

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
                        if (OnMissingOption != null)
                        {
                            OnMissingOption(arg);
                        }
                    }
                    else
                    {
                        options[name] = arg;
                    }
                    name = null;
                }
            }
            return options;
        }
    }
}