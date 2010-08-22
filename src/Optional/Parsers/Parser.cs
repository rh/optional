using System;

namespace Optional.Parsers
{
    public class Parser
    {
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
            var matcher = new PatternMatcher();
            matcher.Register("^-([a-zA-Z0-9]{1})[:=]{1}(.+)$", captures =>
                                                                   {
                                                                       OnShortOption(captures[0]);
                                                                       OnValue(captures[1]);
                                                                   });
            matcher.Register("^-([a-zA-Z0-9]{1})$", captures => OnShortOption(captures[0]));
            matcher.Register("^--([-a-zA-Z0-9]{1,})[:=]{1}([^:=]+)$", captures =>
                                                                          {
                                                                              OnLongOption(captures[0]);
                                                                              OnValue(captures[1]);
                                                                          });
            matcher.Register("^--([-a-zA-Z0-9]{1,})$", captures => OnLongOption(captures[0]));
            matcher.Register("^(.+)$", captures => OnValue(captures[0]));

            foreach (var arg in args)
            {
                matcher.Parse(arg);
            }
        }
    }
}