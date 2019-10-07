using System.Diagnostics.CodeAnalysis;
using FluentDDD.Api;

namespace FluentDDD.ValueObjects.Models
{
    /// <summary>
    ///     <see cref="ValueObject" /> that represents an <c>Point</c> (x, y).
    /// </summary>
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Point : ValueObject
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
        ///     Constructs a <c>Point</c> by the <see cref="Ordinate" /> x and y.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Point(Ordinate x, Ordinate y)
        {
            X = x;
            Y = y;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Constructs a <c>Point</c> as the same <paramref name="point" /> param.
        /// </summary>
        /// <param name="point">The <c>Point</c> to be copied.</param>
        public Point(Point point) : this(point.X, point.Y)
        {
        }

        /// <summary>
        ///     The <see cref="Ordinate" /> X value of the <c>Point</c>.
        /// </summary>
        public Ordinate X { get; }

        /// <summary>
        ///     The <see cref="Ordinate" /> Y value of the <c>Point</c>.
        /// </summary>
        public Ordinate Y { get; }

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