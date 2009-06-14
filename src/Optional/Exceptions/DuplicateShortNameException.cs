using System;

namespace Optional.Exceptions
{
    public class DuplicateShortNameException : Exception
    {
        private readonly string shortName;

        public DuplicateShortNameException(string shortName)
        {
            this.shortName = shortName;
        }

        public string ShortName
        {
            get { return shortName; }
        }
    }
}