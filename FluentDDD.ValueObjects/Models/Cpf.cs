using System;
using System.Diagnostics.CodeAnalysis;
using FluentDDD.Api;
using FluentDDD.ValueObjects.Formatters;
using ValueObjects;
using IFormattable = FluentDDD.Api.Formatter.IFormattable;

namespace FluentDDD.ValueObjects.Models
{
    /// <summary>
    ///     Value Object de <c>Cpf</c>.
    /// </summary>
    /// <inheritdoc cref="ValueObject{TValueObject}" />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class Cpf : ValueObject<Cpf>, IFormattable
    {
        /// <summary>
        ///     Código do <c>Cpf</c> desformatado.
        /// </summary>
        private readonly string _code;

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
            _code = Formatter.Unformat(code);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The default formatter for <c>Cpf</c>.
        /// </summary>
        public IFormatter Formatter { get; }

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
        protected override bool EqualsCore(Cpf other)
        {
            return _code == other._code;
        }

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            return GetType().GetHashCode() * new Random(_code.GetHashCode()).Next();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Formatted();
        }

        public static explicit operator string(Cpf cpf)
        {
            return cpf.Formatted();
        }

        public static implicit operator Cpf(string cpf)
        {
            return new Cpf(cpf);
        }

        public bool Equals(string other)
        {
            return !string.IsNullOrEmpty(other) && Formatter.Unformat(other) == _code;
        }
    }
}