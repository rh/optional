using System;
using System.Reflection;

namespace Optional.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class IgnoreAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(IgnoreAttribute), false);
        }
    }
}