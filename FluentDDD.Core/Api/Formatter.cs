using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using FluentDDD.Internal;

namespace FluentDDD.Api
{
    /// <summary>
    ///     An base class for formatters.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Provides the implementation to the operations <see cref="Format" />, <see cref="Unformat" />,
    ///         <see cref="IsFormatted" /> and <see cref="IsUnformatted" />, if given the patterns to be followed.
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Formatter
    {
        /// <summary>
        ///     Message error for invalid format.
        /// </summary>
        private const string InvalidFormatErrorMessage = "Invalid format. This formatter cannot be used in this value.";

        /// <summary>
        ///     The formatted pattern.
        /// </summary>
        private readonly Regex _formatted;

        /// <summary>
        ///     The replacement pattern to format.
        /// </summary>
        private readonly string _formattedReplacement;

        /// <summary>
        ///     The unformatted pattern.
        /// </summary>
        private readonly Regex _unformatted;

        /// <summary>
        ///     The replacement pattern to unformat.
        /// </summary>
        private readonly string _unformattedReplacement;

        /// <summary>
        ///     Constructs the formatter.
        /// </summary>
        /// <param name="formatted">The formatted pattern.</param>
        /// <param name="unformatted">The replacement pattern to format.</param>
        /// <param name="formattedReplacement">The unformatted pattern.</param>
        /// <param name="unformattedReplacement">The replacement pattern to unformat.</param>
        protected Formatter(Regex formatted, Regex unformatted, string formattedReplacement,
            string unformattedReplacement)
        {
            _formatted = formatted;
            _unformatted = unformatted;
            _formattedReplacement = formattedReplacement;
            _unformattedReplacement = unformattedReplacement;
        }

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

            return IsFormatted(value) ? value : _unformatted.Replace(value, _formattedReplacement);
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

            return IsUnformatted(value) ? value : _formatted.Replace(value, _unformattedReplacement);
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

            return _formatted.IsMatch(value);
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

            return _unformatted.IsMatch(value);
        }

        /// <summary>
        ///     Checks if the <paramref name="value" /> can be used
        ///     by this <c>Formatter</c>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <exception cref="InvalidOperationException">
        ///     The value do not respects the formatted or unformatted patterns.
        /// </exception>
        private void AssertFormattable(string value)
        {
            if (!(_unformatted.IsMatch(value) || _formatted.IsMatch(value)))
                throw new InvalidOperationException(InvalidFormatErrorMessage);
        }
    }
}