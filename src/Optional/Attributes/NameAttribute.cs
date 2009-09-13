using System;

namespace Optional.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class NameAttribute : ValueAttribute
    {
        public NameAttribute(string value)
            : base(value)
        {
        }
    }
}