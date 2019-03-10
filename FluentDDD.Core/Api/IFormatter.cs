using System.Diagnostics.CodeAnalysis;

namespace FluentDDD.Api
{
    /// <summary>
    ///     Representes an formatter.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This interface should be implemented by formatters.
    ///     </para>
    ///     <para>
    ///         This provides an formatted and unformatted functions to transform the <see cref="ValueObject" />s
    ///         attributes and checks if theirs attributes are considerate formattable or not by this formatter.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TType">The primitive type that the formatted or unformatted value are served.</typeparam>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IFormatter<TType>
    {
        /// <summary>
        ///     Formats an value.
        /// </summary>
        /// <param name="value">The value to ve formatted.</param>
        /// <returns>The formatted value.</returns>
        TType Format(TType value);

        /// <summary>
        ///     Unformat an value.
        /// </summary>
        /// <param name="value">The value to be unformatted.</param>
        /// <returns>The unformatted value.</returns>
        TType Unformat(TType value);

        /// <summary>
        ///     Checks if the <paramref name="value" /> is considerate formatted
        ///     by this formatter.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if the <paramref name="value" /> is considerate formatted.</returns>
        bool IsFormatted(TType value);

        /// <summary>
        ///     Checks if the <paramref name="value" /> is considerate unformatted
        ///     by this formatter.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if the <paramref name="value" /> is considerate unformatted.</returns>
        bool IsUnformatted(TType value);
    }
}