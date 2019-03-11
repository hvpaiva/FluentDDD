using System.Diagnostics.CodeAnalysis;

namespace FluentDDD.Api
{
    /// <summary>
    ///     Represents an <see cref="ValueObject{TValueObject}" /> that can be formattable.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The formattable <see cref="ValueObject{TValueObject}" /> should have an base primitive
    ///         type, that the formatted value, in its primitive type, can be served.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TValueObjectType">
    ///     The primitive representative of the <see cref="ValueObject{TValueObject}" /> implementing this interface.
    /// </typeparam>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IFormattable<out TValueObjectType>
    {
        /// <summary>
        ///     The formatted representation, in primitive type,
        ///     of the <see cref="ValueObject{TValueObject}" />.
        /// </summary>
        /// <returns>Its primitive representation formatted.</returns>
        TValueObjectType Formatted();

        /// <summary>
        ///     The unformatted representation, in primitive type,
        ///     of the <see cref="ValueObject{TValueObject}" />.
        /// </summary>
        /// <returns>Its primitive representation unformatted.</returns>
        TValueObjectType Unformatted();
    }
}