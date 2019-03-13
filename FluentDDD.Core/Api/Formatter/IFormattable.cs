using System.Diagnostics.CodeAnalysis;
using ValueObjects;

namespace FluentDDD.Api.Formatter
{
    /// <summary>
    ///     Represents an <see cref="ValueObject{TValueObject}" /> that can be formattable.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IFormattable
    {
        /// <summary>
        ///     The formatter responsible to make the format operations.
        /// </summary>
        IFormatter Formatter { get; }

        /// <summary>
        ///     The formatted representation, in <c>string</c>,
        ///     of the <see cref="ValueObject{TValueObject}" />.
        /// </summary>
        /// <returns>Its <c>string</c> representation formatted.</returns>
        string Formatted();

        /// <summary>
        ///     The unformatted representation, in <c>string</c>,
        ///     of the <see cref="ValueObject{TValueObject}" />.
        /// </summary>
        /// <returns>Its <c>string</c> representation unformatted.</returns>
        string Unformatted();
    }
}