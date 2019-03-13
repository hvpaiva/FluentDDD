using System;
using System.Diagnostics.CodeAnalysis;
using FluentDDD.Internal;
using ValueObjects;

namespace FluentDDD.Api.Formatter
{
    /// <summary>
    ///     An base class for formatters.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Provides the implementation to the operations <see cref="Format" />, <see cref="Unformat" />,
    ///         <see cref="IsFormatted" />, <see cref="IsUnformatted" />, <see cref="IsFormattable" /> and
    ///         <see cref="AssertFormattable" /> if given the patterns to be followed.
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Formatter : IFormatter
    {
        /// <summary>
        ///     Constructs the base formatter.
        /// </summary>
        /// <param name="formattedFormat">The formatted format.</param>
        /// <param name="unformattedFormat">The unformatted format.</param>
        protected Formatter(IFormat formattedFormat, IFormat unformattedFormat)
        {
            FormattedFormat = formattedFormat;
            UnformattedFormat = unformattedFormat;
        }

        /// <summary>
        ///     The formatted format.
        /// </summary>
        public IFormat FormattedFormat { get; }

        /// <summary>
        ///     The unformatted format.
        /// </summary>
        public IFormat UnformattedFormat { get; }

        /// <summary>
        ///     Formats an value.
        /// </summary>
        /// <param name="value">The value to ve formatted.</param>
        /// <returns>The formatted value.</returns>
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Throw if the <paramref name="value" /> can't be used in this formatter.
        /// </exception>
        public string Format(string value)
        {
            value.Guard(nameof(value));
            AssertFormattable(value);

            return IsFormatted(value)
                ? value
                : UnformattedFormat.Pattern.Replace(value, FormattedFormat.Replacement);
        }

        /// <summary>
        ///     Unformat an value.
        /// </summary>
        /// <param name="value">The value to be unformatted.</param>
        /// <returns>The unformatted value.</returns>
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Throw if the <paramref name="value" /> can't be used in this formatter.
        /// </exception>
        public string Unformat(string value)
        {
            value.Guard(nameof(value));
            AssertFormattable(value);

            return IsUnformatted(value)
                ? value
                : FormattedFormat.Pattern.Replace(value, UnformattedFormat.Replacement);
        }

        /// <summary>
        ///     Checks if the <paramref name="value" /> is considerate formatted
        ///     by this formatter.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if the <paramref name="value" /> is considerate formatted.</returns>
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty.
        /// </exception>
        public bool IsFormatted(string value)
        {
            value.Guard(nameof(value));

            return FormattedFormat.Pattern.IsMatch(value);
        }

        /// <summary>
        ///     Checks if the <paramref name="value" /> is considerate unformatted
        ///     by this formatter.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if the <paramref name="value" /> is considerate unformatted.</returns>
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty.
        /// </exception>
        public bool IsUnformatted(string value)
        {
            value.Guard(nameof(value));

            return UnformattedFormat.Pattern.IsMatch(value);
        }

        /// <summary>
        ///     Checks if the <paramref name="value" /> can be used
        ///     in this <c>Formatter</c>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if the value checks the formats expected.</returns>
        public bool IsFormattable(string value)
        {
            return IsFormatted(value) || IsUnformatted(value);
        }

        /// <summary>
        ///     Asserts if the <paramref name="value" /> can be used
        ///     by this <c>Formatter</c>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <exception cref="InvalidOperationException">
        ///     The value do not respects the formatted or unformatted patterns.
        /// </exception>
        public void AssertFormattable(string value)
        {
            if (!IsFormattable(value))
                throw new InvalidOperationException("Invalid format for this formatter.");
        }
    }
}