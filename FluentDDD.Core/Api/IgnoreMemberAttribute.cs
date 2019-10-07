using System;

namespace FluentDDD.Api
{
    /// <inheritdoc />
    /// <summary>
    ///     An field or property marked with this attribute will be ignored by
    ///     the ValueObject comparator as a target to comparision.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}