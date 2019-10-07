using System.Diagnostics.CodeAnalysis;
using FluentDDD.Api;
using FluentDDD.ValueObjects.Formatters;
using FluentFormatter;

namespace FluentDDD.ValueObjects.Models
{
    /// <summary>
    ///     Value Object de <c>Cpf</c>.
    /// </summary>
    /// <inheritdoc cref="ValueObject" />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class Cpf : ValueObject, IFormattable
    {
        /// <summary>
        ///     Constructs the <c>Cpf</c>.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="code" /> can be formatted or unformatted, bud should be an valid <c>Cpf</c>.
        /// </remarks>
        /// <param name="code">The <c>Cpf</c> code. Can be sended formatted or unformatted.</param>
        public Cpf(string code)
        {
            Formatter = new CpfFormatter();
            Code = Formatter.Unformat(code);
        }

        /// <summary>
        ///     Código do <c>Cpf</c> desformatado.
        /// </summary>
        public string Code { get; }

        /// <inheritdoc />
        /// <summary>
        ///     The default formatter for <c>Cpf</c>.
        /// </summary>
        [IgnoreMember]
        public IFormatter Formatter { get; }

        /// <inheritdoc />
        public string Formatted()
        {
            return Formatter.Format(Code);
        }

        /// <inheritdoc />
        public string Unformatted()
        {
            return Formatter.Unformat(Code);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Formatted();
        }
    }
}