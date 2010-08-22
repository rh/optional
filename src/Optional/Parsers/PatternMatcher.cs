using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Optional.Parsers
{
    public class PatternMatcher
    {
        private readonly List<string> patterns = new List<string>();
        private readonly Dictionary<string, Action<IList<string>>> actions = new Dictionary<string, Action<IList<string>>>();

        public void Register(string pattern, Action<IList<string>> action)
        {
            patterns.Add(pattern);
            actions[pattern] = action;
        }

        public void Parse(string input)
        {
            foreach (var pattern in patterns)
            {
                var matches = Regex.Matches(input, pattern);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        var captures = new List<string>();
                        for (var i = 1; i < match.Groups.Count; i++)
                        {
                            var capture = match.Groups[i].Value;
                            captures.Add(capture);
                        }
                        actions[pattern](captures);
                    }

                    break;
                }
            }
        }
    }
}