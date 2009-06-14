using System;

namespace Optional.Exceptions
{
    /// <summary>
    /// This <see cref="Exception"/> is thrown when an option is given that is not registered.
    /// </summary>
    public class InvalidOptionException : Exception
    {
        private readonly string name;

        public InvalidOptionException(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Gets the name of the invalid option.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return string.Format("Invalid option: '{0}'.", Name);
        }
    }
}