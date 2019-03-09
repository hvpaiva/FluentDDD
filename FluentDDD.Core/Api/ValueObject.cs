using System;
using System.Diagnostics.CodeAnalysis;

namespace FluentDDD.Api
{
    /// <inheritdoc />
    /// <summary>
    ///     Classe base para Value Objects.
    /// </summary>
    /// <remarks>
    ///     Objetos <c>ValueObject</c> são imutáveis e são igualáveis por seus valores.
    ///     Diferente das <see cref="T:FluentDDD.Core.Api.Entity`1" /> que são comparados por seus identificadores.
    /// </remarks>
    [Serializable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public abstract class ValueObject : IValueObject
    {
        #region ToChildImplementation

        /// <inheritdoc />
        public abstract IValueObject Copy();

        /// <summary>
        ///     Confere a igualdade deste <c>ValueObject</c> com o
        ///     dado <c>ValueObject</c> <paramref name="other" />.
        /// </summary>
        /// <remarks>
        ///     <c>ValueObject</c>s são considerados iguais quando o estado de seus
        ///     atributos são os mesmos.
        /// </remarks>
        /// <param name="other">O <c>ValueObject</c> no qual será comparado com este.</param>
        /// <returns><c>true</c> se os atributos do <c>ValueObject</c> são iguais.</returns>
        protected abstract bool Equals(ValueObject other);

        /// <summary>
        ///     Função de Hash para o <c>ValueObject</c>.
        /// </summary>
        /// <returns>O valor do Hash.</returns>
        protected abstract int GetHashCodeCore();

        #endregion

        #region System.Object overrides

        /// <summary>
        ///     Confere se dois <c>ValueObject</c>s são considerados iguais. Sobrescrevendo o operador <c>==</c>.
        /// </summary>
        /// <param name="vo1">O <c>ValueObject</c> um.</param>
        /// <param name="vo2">O <c>ValueObject</c> dois.</param>
        /// <returns><c>true</c> se for considerado igual, ou <c>false</c> senão.</returns>
        public static bool operator ==(ValueObject vo1, ValueObject vo2)
        {
            return ReferenceEquals(vo1, null) && ReferenceEquals(vo2, null)
                   || !ReferenceEquals(vo1, null) && !ReferenceEquals(vo2, null) && vo1.Equals(vo2);
        }

        /// <summary>
        ///     Confere se dois Value Objects são considerados diferentes. Sobrescrevendo o operador <c>!=</c>.
        /// </summary>
        /// <param name="a">O Value Object.</param>
        /// <param name="b">O Value Object.</param>
        /// <returns><c>true</c> se for não considerado igual, ou <c>false</c> se for igual.</returns>
        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        /// <summary>
        ///     Confere a igualdade deste <c>ValueObject</c> com o
        ///     dado <c>object</c> <paramref name="obj" />.
        /// </summary>
        /// <remarks>
        ///     <c>ValueObject</c>s são considerados iguais quando o estado de seus
        ///     atributos são os mesmos.
        /// </remarks>
        /// <param name="obj">O <c>ValueObject</c> no qual será comparado com este.</param>
        /// <returns><c>true</c> se os atributos do <c>ValueObject</c> são iguais.</returns>
        public sealed override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || !ReferenceEquals(null, obj)
                   && obj is ValueObject valueObject && Equals(valueObject);
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        #endregion
    }
}