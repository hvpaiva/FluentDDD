using FluentDDD.ValueObjects.Models;
using FluentFormatter;

namespace FluentDDD.ValueObjects.Formats
{
    /// <summary>
    ///     The representation of the <see cref="Cpf" /> formats.
    /// </summary>
    public static class CpfFormat
    {
        /// <summary>
        ///     The pattern for CPF formatted.
        /// </summary>
        /// <example>114.582.016-60</example>
        public static IFormat Formatted => new Format(@"^(\d{3})[.](\d{3})[.](\d{3})-(\d{2})$", "$1.$2.$3-$4");

        /// <summary>
        ///     The pattern for CPF unformatted.
        /// </summary>
        /// <example>11458201660</example>
        public static IFormat Unformatted => new Format(@"^(\d{3})(\d{3})(\d{3})(\d{2})$", "$1$2$3$4");
    }
}