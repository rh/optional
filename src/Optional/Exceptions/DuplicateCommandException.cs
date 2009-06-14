using System;

namespace Optional.Exceptions
{
    public class DuplicateCommandException : Exception
    {
        private readonly string name;

        public DuplicateCommandException(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return string.Format("Command '{0}' was already registered.", Name);
        }
    }
}