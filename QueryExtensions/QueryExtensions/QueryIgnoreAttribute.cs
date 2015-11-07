using System;

namespace QueryExtensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class QueryIgnoreAttribute : Attribute
    {
    }
}
