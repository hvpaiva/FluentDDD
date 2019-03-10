using System;
using System.Diagnostics.CodeAnalysis;

namespace FluentDDD.Api
{
    /// <summary>
    ///     A lightweight class for Entities.
    ///     Classe base para representar uma entidade de domínio.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         An <c>Entity</c> should <B>ALWAYS</B> have an identity of type
    ///         <see cref="IValueObject" />.
    ///     </para>
    ///     <para>
    ///         The <see cref="Identity" /> of <c>Entity</c> can't be changed, because if
    ///         we change the <see cref="Identity"/> of an <c>Entity</c> it means that
    ///         it is another <c>Entity</c>.
    ///     </para>
    ///     <para>
    ///         Different from <see cref="ValueObject" />, an <c>Entity</c> is comparable
    ///         by its <see cref="Identity"/>. That means if two <c>Entity</c> have same
    ///         <see cref="Identity"/> value, the both are considerate the same. Even if their
    ///         another attributes have not the same value.
    ///     </para>
    /// </remarks>
    /// <seealso cref="IValueObject" />
    /// <typeparam name="TId">The <see cref="Identity" /> of the <c>Entity</c>.</typeparam>
    [Serializable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public abstract class Entity<TId> where TId : IValueObject
    {
        /// <summary>
        ///     Constructs an <c>Entity</c> with its <see cref="Identity"/>.
        /// </summary>
        /// <seealso cref="IValueObject" />
        /// <param name="identity">The identity of the <c>Entity</c>.</param>
        protected Entity(TId identity)
        {
            Identity = identity;
        }

        /// <summary>
        ///     The getter for the <see cref="IValueObject"/> identity of
        ///     the <c>Entity</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The <see cref="Identity"/> attribute is immutable, because if we change
        ///         the <see cref="Identity"/> of an <c>Entity</c>, the <c>Entity</c> are
        ///         considerate a new one.
        ///     </para>
        /// </remarks>
        public TId Identity { get; }

        /// <summary>
        ///     Checks if the <c>Entity</c> is valid.
        /// </summary>
        /// <returns><c>true</c> if the <c>Entity</c> is valid.</returns>
        public abstract bool IsValid();

        /// <summary>
        ///     Checks the equality of two <c>Entity</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Two <c>Entity</c> are comparable by their <see cref="Identity"/>.
        ///     </para>
        /// </remarks>
        /// <param name="entity">The target <c>Entity</c> to be comparable with this.</param>
        /// <returns><c>true</c> if the two <c>Entity</c> have the same <see cref="Identity" /> value.</returns>
        public bool Equals(Entity<TId> entity)
        {
            return ReferenceEquals(this, entity)
                   || !ReferenceEquals(null, entity)
                   && Identity.Equals(entity.Identity);
        }

        #region System.Object overrides

        /// <summary>
        ///     Checks the equality of two <c>Entity</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Two <c>Entity</c> are comparable by their <see cref="Identity"/>.
        ///     </para>
        /// </remarks>
        /// <param name="obj">The target <c>Entity</c> to be comparable with this.</param>
        /// <returns><c>true</c> if the two <c>Entity</c> have the same <see cref="Identity" /> value.</returns>
        public sealed override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                   || !ReferenceEquals(null, obj)
                   && obj is Entity<TId> entity
                   && Equals(entity);
        }

        /// <summary>
        ///      Checks the equality of two <c>Entity</c>. Overrides the <c>==</c> operator.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Two <c>Entity</c> are comparable by their <see cref="Identity"/>.
        ///     </para>
        /// </remarks>
        /// <param name="a">The <c>Entity</c> a.</param>
        /// <param name="b">The <c>Entity</c> b.</param>
        /// <returns><c>true</c> if the two <c>Entity</c> have the same <see cref="Identity" /> value.</returns>
        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            return ReferenceEquals(a, null) && ReferenceEquals(b, null)
                   || !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);
        }

        /// <summary>
        ///      Checks if two <c>Entity</c> are <B>NOT</B> considerate equals. Overrides the <c>==</c> operator.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Two <c>Entity</c> are comparable by their <see cref="Identity"/>.
        ///     </para>
        /// </remarks>
        /// <param name="a">The <c>Entity</c> a.</param>
        /// <param name="b">The <c>Entity</c> b.</param>
        /// <returns><c>true</c> if the two <c>Entity</c> have <B>NOT</B> the same <see cref="Identity" /> value.</returns>
        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            return GetType().GetHashCode() * new Random(Identity.GetHashCode()).Next();
        }

        /// <summary>
        ///     The <c>string</c> representation of the <c>Entity</c>.
        /// </summary>
        /// <returns>Its <c>string</c> representation.</returns>
        public override string ToString()
        {
            return $"{GetType().Name} [Id = {Identity}]";
        }

        #endregion
    }
}