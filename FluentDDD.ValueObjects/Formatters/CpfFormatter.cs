using System.Diagnostics.CodeAnalysis;
using FluentDDD.Api.Formatter;
using FluentDDD.ValueObjects.Formats;
using FluentDDD.ValueObjects.Models;

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