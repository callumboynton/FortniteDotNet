using System;

namespace FortniteDotNet.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class ValueAttribute : Attribute
    {
        internal string Value { get; }
        
        internal ValueAttribute(string value)
        {
            Value = value;
        }
    }
}