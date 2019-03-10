using FluentDDD.ValueObjects.Models;

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
        public static string CpfFormatted => @"^(\d{3})[.](\d{3})[.](\d{3})-(\d{2})$";

        /// <summary>
        ///     The pattern for CPF unformatted.
        /// </summary>
        /// <example>11458201660</example>
        public static string CpfUnformatted => @"^(\d{3})(\d{3})(\d{3})(\d{2})$";
    }
}