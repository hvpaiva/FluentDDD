using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using FluentDDD.Api;

namespace FluentDDD.ValueObjects.Models
{
    /// <summary>
    ///     Represents an <c>Ordinate</c> value.
    /// </summary>
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Ordinate : ValueObject<Ordinate>
    {
        /// <summary>
        ///     The default ordinate is 0.
        /// </summary>
        public static readonly Ordinate DefaultOrdinate = new Ordinate(0);

        /// <summary>
        ///     Constructs the <c>Ordinate</c> value.
        /// </summary>
        /// <param name="value">The value of the ordinate.</param>
        public Ordinate(double value)
        {
            Value = value;
        }

        /// <summary>
        ///     The ordinate value.
        /// </summary>
        private double Value { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Checks if this <c>Ordinate</c> is equals the another
        ///     <c>Ordinate</c> <paramref name="other" />.
        /// </summary>
        /// <param name="other">The other <c>Ordinate</c>.</param>
        /// <returns><c>true</c> if the <see cref="Value" /> of both <c>Ordinate</c>s are the same.</returns>
        protected override bool EqualsCore(Ordinate other)
        {
            return Math.Abs(Value - other.Value) < 0;
        }

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            return GetType().GetHashCode() * new Random(Value.GetHashCode()).Next();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}