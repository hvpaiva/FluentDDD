using System;
using System.Diagnostics.CodeAnalysis;
using FluentDDD.Api;
using FluentDDD.ValueObjects.Formatters;

namespace FluentDDD.ValueObjects.Models
{
    /// <summary>
    ///     Value Object de <c>Cpf</c>.
    /// </summary>
    /// <inheritdoc cref="ValueObject{TValueObject}" />
    /// <inheritdoc cref="IFormattable{TValueObjectType}" />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class Cpf : ValueObject<Cpf>, IFormattable<string>
    {
        /// <summary>
        ///     Código do <c>Cpf</c> desformatado.
        /// </summary>
        private readonly string _code;

        /// <summary>
        ///     The default formatter for <c>Cpf</c>.
        /// </summary>
        private readonly Formatter _formatter;

        /// <summary>
        ///     Constructs the<c>Cpf</c>.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="code" /> can be formatted or unformatted, bud should be an valid <c>Cpf</c>.
        /// </remarks>
        /// <param name="code">The <c>Cpf</c> code. Can be formatted or unformatted.</param>
        public Cpf(string code)
        {
            _formatter = new CpfFormatter();
            _code = _formatter.Unformat(code);
        }

        /// <inheritdoc />
        public string Formatted()
        {
            return _formatter.Format(_code);
        }

        /// <inheritdoc />
        public string Unformatted()
        {
            return _formatter.Unformat(_code);
        }

        /// <inheritdoc />
        protected override bool Equals(Cpf other)
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
            return _formatter.Format(_code);
        }
    }
}