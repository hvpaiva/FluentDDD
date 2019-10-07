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
    public class Ordinate : ValueObject
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
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}