using System;

namespace Optional.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public abstract class ValueAttribute : Attribute
    {
        public string Value { get; set; }

        protected ValueAttribute(string value)
        {
            Value = value;
        }
    }
}