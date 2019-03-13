using System;
using System.Diagnostics.CodeAnalysis;
using FluentDDD.Internal;

namespace FluentDDD.Api
{
    /// <inheritdoc />
    /// <summary>
    ///     A lightweight class for Value Objects.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <c>ValueObject</c>s are immutable and differentiable from others <c>ValueObject</c>s
    ///         by comparing the state of its attributes, that means that an <c>ValueObject</c> is
    ///         equals another if its attributes have the same value of the another <c>ValueObject</c>,
    ///         and the attributes, once initiated, can't be changed.
    ///     </para>
    ///     <para>
    ///         An <c>ValueObject</c> is <B>ALWAYS</B> immutable and should
    ///         give an way to copy itself.
    ///     </para>
    /// </remarks>
    [Serializable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public abstract class ValueObject<TValueObject> : ICloneable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {
        /// <inheritdoc />
        public TValueObject Clone()
        {
            return (TValueObject) MemberwiseClone();
        }

        #region ToChildImplementation

        /// <summary>
        ///     Checks if this <c>ValueObject</c> is equals the
        ///     target <c>ValueObject</c> <paramref name="other" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>ValueObject</c>s are considerate equals when the state of its
        ///         attributes are the same.
        ///     </para>
        /// </remarks>
        /// <param name="other">The target <c>ValueObject</c> for comparison.</param>
        /// <returns><c>true</c> if the attributes of both <c>ValueObject</c>s are equals.</returns>
        protected abstract bool EqualsCore(TValueObject other);

        /// <summary>
        ///     Hash function for the <c>ValueObject</c>.
        /// </summary>
        /// <returns>The hash code.</returns>
        protected abstract int GetHashCodeCore();

        #endregion

        #region System.Object overrides

        /// <summary>
        ///     Checks if two <c>ValueObject</c>s are considerate equals. Overrides the <c>==</c> operator.
        /// </summary>
        /// <param name="vo1">The <c>ValueObject</c> one.</param>
        /// <param name="vo2">The <c>ValueObject</c> two.</param>
        /// <returns><c>true</c> if the attributes of both <c>ValueObject</c>s are equals.</returns>
        public static bool operator ==(ValueObject<TValueObject> vo1, ValueObject<TValueObject> vo2)
        {
            return ReferenceEquals(vo1, null) && ReferenceEquals(vo2, null)
                   || !ReferenceEquals(vo1, null) && !ReferenceEquals(vo2, null) && vo1.Equals(vo2);
        }

        /// <summary>
        ///     Checks if two <c>ValueObject</c>s are <B>NOT</B> considerate equals. Overrides the <c>==</c> operator.
        /// </summary>
        /// <param name="vo1">The <c>ValueObject</c> one.</param>
        /// <param name="vo2">The <c>ValueObject</c> two.</param>
        /// <returns><c>true</c> if the attributes of both <c>ValueObject</c>s are <B>NOT</B> the same.</returns>
        public static bool operator !=(ValueObject<TValueObject> vo1, ValueObject<TValueObject> vo2)
        {
            return !(vo1 == vo2);
        }

        /// <summary>
        ///     Checks if this <c>ValueObject</c> is equals the
        ///     target <c>object</c> <paramref name="obj" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>ValueObject</c>s are considerate equals when the state of its
        ///         attributes are the same.
        ///     </para>
        /// </remarks>
        /// <param name="obj">The target <c>ValueObject</c> for comparison.</param>
        /// <returns><c>true</c> if the attributes of both <c>ValueObject</c>s are equals.</returns>
        public sealed override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || !ReferenceEquals(null, obj)
                   && obj is TValueObject valueObject && EqualsCore(valueObject);
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        #endregion
    }
}