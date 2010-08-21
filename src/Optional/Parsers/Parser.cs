using System;
using System.Text.RegularExpressions;

namespace Optional.Parsers
{
    public class Parser
    {
        public static Regex ShortOption = new Regex("^-([a-zA-Z0-9]{1})$");
        public static Regex ShortOptionWithValue = new Regex("^-([a-zA-Z0-9]{1})[:=]{1}(.+)$");
        public static Regex LongOption = new Regex("^--([-a-zA-Z0-9]{1,})$");
        public static Regex LongOptionWithValue = new Regex("^--([-a-zA-Z0-9]{1,})[:=]{1}([^:=]+)$");

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
            foreach (var arg in args)
            {
                if (ShortOptionWithValueMatches(arg))
                {
                    continue;
                }

                if (ShortOptionMatches(arg))
                {
                    continue;
                }

                if (LongOptionWithValueMatches(arg))
                {
                    continue;
                }

                if (LongOptionMatches(arg))
                {
                    continue;
                }

                OnValue(arg);
            }
        }

        private bool ShortOptionWithValueMatches(string input)
        {
            var matches = ShortOptionWithValue.Matches(input);
            if (matches.Count == 1 && matches[0].Groups.Count == 3)
            {
                OnShortOption(matches[0].Groups[1].Value);
                OnValue(matches[0].Groups[2].Value);
                return true;
            }

            return false;
        }

        private bool ShortOptionMatches(string input)
        {
            var matches = ShortOption.Matches(input);
            if (matches.Count == 1 && matches[0].Groups.Count == 2)
            {
                OnShortOption(matches[0].Groups[1].Value);
                return true;
            }

            return false;
        }

        private bool LongOptionWithValueMatches(string input)
        {
            var matches = LongOptionWithValue.Matches(input);
            if (matches.Count == 1 && matches[0].Groups.Count == 3)
            {
                OnLongOption(matches[0].Groups[1].Value);
                OnValue(matches[0].Groups[2].Value);
                return true;
            }

            return false;
        }

        private bool LongOptionMatches(string input)
        {
            var matches = LongOption.Matches(input);
            if (matches.Count == 1 && matches[0].Groups.Count == 2)
            {
                OnLongOption(matches[0].Groups[1].Value);
                return true;
            }

            return false;
        }
    }
}