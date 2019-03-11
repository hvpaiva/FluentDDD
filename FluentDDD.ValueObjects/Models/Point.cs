using System;
using FluentDDD.Api;

namespace FluentDDD.ValueObjects.Models
{
    /// <summary>
    ///     <see cref="ValueObject{TValueObject}" /> that represents an <c>Point</c> (x, y).
    /// </summary>
    /// <inheritdoc />
    public class Point : ValueObject<Point>
    {
        /// <summary>
        ///     Constructs the <c>Point</c> by the <see cref="X" /> and <see cref="Y" /> value.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Point(double x, double y)
        {
            X = new Ordinate(x);
            Y = new Ordinate(y);
        }

        /// <summary>
        ///     Constructs a default point (0, 0).
        /// </summary>
        public Point()
        {
            X = Ordinate.DefaultOrdinate;
            Y = Ordinate.DefaultOrdinate;
        }

        /// <summary>
        ///     The <see cref="Ordinate" /> X value of the <c>Point</c>.
        /// </summary>
        public Ordinate X { get; }

        /// <summary>
        ///     The <see cref="Ordinate" /> Y value of the <c>Point</c>.
        /// </summary>
        public Ordinate Y { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Checks if the <c>Point</c> is equals the another <c>Point</c>
        ///     <paramref name="other" />.
        /// </summary>
        /// <param name="other">The other <c>Point</c> to check.</param>
        /// <returns>
        ///     <c>true</c> if this <see cref="P:FluentDDD.ValueObjects.Models.Point.X" /> and
        ///     <see cref="P:FluentDDD.ValueObjects.Models.Point.Y" /> are equals
        ///     to the <paramref name="other" />'s one.
        /// </returns>
        protected override bool EqualsCore(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            return GetType().GetHashCode() * new Random(X.GetHashCode() + Y.GetHashCode()).Next();
        }

        /// <summary>
        ///     Provides a formatted representation of the <c>Point</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The format is '(x, y)'.
        ///     </para>
        /// </remarks>
        /// <returns>The <c>Point</c> as (x, y).</returns>
        public override string ToString()
        {
            return $@"({X:R}, {Y:R})";
        }
    }
}