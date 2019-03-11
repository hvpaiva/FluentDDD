using System.Diagnostics.CodeAnalysis;

namespace FluentDDD.Api
{
    /// <summary>
    ///     Represents an Aggregate Root. An <c>AggregateRoot</c> <B>IS</B> an <see cref="Entity{TId}" />, so
    ///     it follows all rules from <see cref="Entity{TId}" />. Also, the <c>AggregateRoot</c>
    ///     represents a <B>Boundary Context</B> and the entities correlated to this <c>AggregateRoot</c>
    ///     should not be accessed directly from outside, but by its <c>AggregateRoot</c>'s reference.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : ValueObject<TId>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Constructs an <c>AggregateRoot</c> with its <paramref name="identity" />.
        /// </summary>
        /// <param name="identity">
        ///     The <see cref="T:FluentDDD.Api.ValueObject`1" /> that represents the Identity
        ///     of the <c>AggregateRoot</c>.
        /// </param>
        protected AggregateRoot(TId identity) : base(identity)
        {
        }
    }
}