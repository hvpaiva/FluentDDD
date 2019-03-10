using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace FluentDDD.Api
{
    /// <inheritdoc />
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
    public class Formatter : IFormatter<string>
    {
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

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty or if the format
        ///     can't be used by this formatter.
        /// </exception>
        public virtual string Format(string value)
        {
            ValidateValue(value);

            if (!_unformatted.IsMatch(value) && !_formatted.IsMatch(value))
                throw new ArgumentException("Invalid format.");

            return IsFormatted(value) ? value : _unformatted.Replace(value, _formattedReplacement);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty or if the format
        ///     can't be used by this formatter.
        /// </exception>
        public virtual string Unformat(string value)
        {
            ValidateValue(value);

            if (!_unformatted.IsMatch(value) && !_formatted.IsMatch(value))
                throw new ArgumentException("Invalid format.");

            return IsUnformatted(value) ? value : _formatted.Replace(value, _unformattedReplacement);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty.
        /// </exception>
        public virtual bool IsFormatted(string value)
        {
            ValidateValue(value);

            return _formatted.IsMatch(value);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty.
        /// </exception>
        public virtual bool IsUnformatted(string value)
        {
            ValidateValue(value);

            return _unformatted.IsMatch(value);
        }

        /// <summary>
        ///     Checks if the value passes to the condition to be used by its formatter.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <exception cref="ArgumentException">
        ///     Throw if the value is null or empty.
        /// </exception>
        private static void ValidateValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value can't be empty or null.");
        }
    }
}