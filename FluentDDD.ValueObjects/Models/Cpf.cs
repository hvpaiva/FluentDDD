using System;
using System.Diagnostics.CodeAnalysis;
using FluentDDD.Api;
using FluentDDD.ValueObjects.Formatters;

namespace FluentDDD.ValueObjects.Models
{
    /// <summary>
    ///     Value Object de <c>Cpf</c>.
    /// </summary>
    /// <inheritdoc cref="ValueObject" />
    /// <inheritdoc cref="IFormattable{TValueObjectType}" />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class Cpf : ValueObject, IFormattable<string>
    {
        /// <summary>
        ///     Código do <c>Cpf</c> desformatado.
        /// </summary>
        private readonly string _code;

        /// <summary>
        ///     Constructs the<c>Cpf</c>.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="code" /> can be formatted or unformatted, bud should be an valid <c>Cpf</c>.
        /// </remarks>
        /// <param name="code">The <c>Cpf</c> code. Can be formatted or unformatted.</param>
        public Cpf(string code)
        {
            _code = Formatter.Unformat(code);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The default formatter for <c>Cpf</c>.
        /// </summary>
        public Formatter Formatter => new CpfFormatter();

        /// <inheritdoc />
        public string Formatted()
        {
            return Formatter.Format(_code);
        }

        /// <inheritdoc />
        public string Unformatted()
        {
            return Formatter.Unformat(_code);
        }

        /// <inheritdoc />
        public override ValueObject Copy()
        {
            return (Cpf) MemberwiseClone();
        }

        /// <inheritdoc />
        protected override bool Equals(ValueObject other)
        {
            return other is Cpf cpf && _code == cpf._code;
        }

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            return GetType().GetHashCode() * new Random(_code.GetHashCode()).Next();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _code;
        }
    }
}