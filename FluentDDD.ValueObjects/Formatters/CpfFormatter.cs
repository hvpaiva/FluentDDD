using System.Diagnostics.CodeAnalysis;
using FluentDDD.ValueObjects.Formats;
using FluentDDD.ValueObjects.Models;
using FluentFormatter;

namespace FluentDDD.ValueObjects.Formatters
{
    /// <summary>
    ///     The <see cref="Cpf" /> formatter.
    /// </summary>
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class CpfFormatter : Formatter
    {
        /// <summary>
        ///     Constructs the <see cref="CpfFormatter" />
        /// </summary>
        /// <inheritdoc />
        public CpfFormatter() : base(CpfFormat.Formatted, CpfFormat.Unformatted)
        {
        }
    }
}