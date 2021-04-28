using System;

namespace FortniteDotNet.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class RequiredFieldsAttribute : Attribute
    {
        internal string[] Fields { get; }
        
        internal RequiredFieldsAttribute(params string[] fields)
        {
            Fields = fields;
        }
    }
}