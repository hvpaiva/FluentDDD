using System;
using System.Diagnostics.CodeAnalysis;
using FluentDDD.Api;

namespace FluentDDD.ValueObjects.Models
{
    /// <inheritdoc />
    /// <summary>
    ///     Default Identity <see cref="T:FluentDDD.Api.ValueObject`1" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Some <see cref="T:FluentDDD.Api.Entity`1" /> needs a <c>Identity</c>, but can't have a logical
    ///         <see cref="T:FluentDDD.Api.ValueObject`1" /> for this. So, this <c>Identity</c>
    ///         <see cref="T:FluentDDD.Api.ValueObject`1" /> is an generic <c>Identity</c> for those entities.
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Identity : ValueObject<Identity>
    {
        /// <summary>
        ///     Constructs the <c>Identity</c>.
        /// </summary>
        public Identity()
        {
            Value = new Guid();
        }

        /// <summary>
        ///     The getter of the <c>Identity</c> value.
        /// </summary>
        public Guid Value { get; }

        /// <inheritdoc />
        protected override bool EqualsCore(Identity other)
        {
            return Value == other.Value;
        }

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            return GetType().GetHashCode() * new Random(Value.GetHashCode()).Next();
        }
    }
}