using System;

namespace Optional.Attributes
{
    /// <summary>
    /// Marks a property as the default option, i.e. if only one value is given
    /// on the command-line, this property is set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DefaultAttribute : Attribute
    {
    }
}