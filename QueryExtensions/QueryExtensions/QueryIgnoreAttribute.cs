using System;

namespace QueryExtensions
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class QueryIgnoreAttribute : Attribute
    {
    }
}
