using System;
using System.Text.RegularExpressions;

namespace Optional.Parsers
{
    public class Parser
    {
        public static Regex ShortOption = new Regex("^-[a-zA-Z0-9]{1}$");
        public static Regex ShortOptionWithValue = new Regex("^-[a-zA-Z0-9]{1}[:=]{1}(.+)$");
        public static Regex LongOption = new Regex("^--[-a-zA-Z0-9]{1,}$");
        public static Regex LongOptionWithValue = new Regex("^--[-a-zA-Z0-9]{1,}[:=]{1}([^:=]+)$");

        public Action<string> OnShortOption = name => { };
        public Action<string> OnLongOption = name => { };
        public Action<string> OnValue = value => { };

        /// <summary>
        /// Parses <see cref="args"/> and calls <see cref="OnShortOption"/>, <see cref="OnLongOption"/>
        /// and <see cref="OnValue"/> for every short option, long option and value encountered.
        /// </summary>
        /// <param name="args">The command-line arguments of the application.</param>
        public void Parse(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (ShortOptionWithValue.IsMatch(arg))
                {
                    var values = Regex.Split(arg, "[:=]");
                    OnShortOption(values[0].Substring(1));
                    OnValue(values[1]);
                }
                else if (ShortOption.IsMatch(arg))
                {
                    OnShortOption(arg.Substring(1));
                }
                else if (LongOptionWithValue.IsMatch(arg))
                {
                    var values = Regex.Split(arg, "[:=]");
                    OnLongOption(values[0].Substring(2));
                    OnValue(values[1]);
                }
                else if (LongOption.IsMatch(arg))
                {
                    OnLongOption(arg.Substring(2));
                }
                else
                {
                    OnValue(arg);
                }
            }
        }
    }
}