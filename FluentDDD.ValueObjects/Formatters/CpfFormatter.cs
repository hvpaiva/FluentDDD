using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using FluentDDD.Api;
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
        public CpfFormatter()
            : base(
                new Regex(CpfFormat.CpfFormatted, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                new Regex(CpfFormat.CpfUnformatted, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                "$1.$2.$3-$4",
                "$1$2$3$4"
            )
        {
        }
    }
}