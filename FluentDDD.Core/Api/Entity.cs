using System;
using System.Diagnostics.CodeAnalysis;

namespace FluentDDD.Api
{
    /// <summary>
    ///     Classe base para representar uma entidade de domínio.
    /// </summary>
    /// <remarks>
    ///     Uma <c>Entity</c> deve sempre possuir um identificador
    ///     do tipo <see cref="IValueObject" />.
    ///     <para>
    ///         Diferente de um <see cref="ValueObject" />, uma <c>Entity</c> é comparada
    ///         por sua identidade, ou seja, se o valor de sua identidade for igual
    ///         à identidade de outra <c>Entity</c>, estas são consideradas iguais,
    ///         independente de seus outros atributos.
    ///     </para>
    ///     <para>
    ///         A <see cref="Identity" /> da <c>Entity</c> é <B>imutável</B>, já que ao mudar
    ///         sua identidade, na verdade teremos outra <c>Entity</c> diferente.
    ///     </para>
    /// </remarks>
    /// <seealso cref="IValueObject" />
    /// <typeparam name="TId">A <see cref="Identity" /> da <c>Entity</c>.</typeparam>
    [Serializable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public abstract class Entity<TId> where TId : IValueObject
    {
        /// <summary>
        ///     Constrói uma <c>Entity</c> com seu <see cref="Identity" />.
        /// </summary>
        /// <seealso cref="IValueObject" />
        /// <param name="identity">A identidade da <c>Entity</c>.</param>
        protected Entity(TId identity)
        {
            Identity = identity;
        }

        /// <summary>
        ///     O <see cref="IValueObject" /> identificador da <c>Entity</c>.
        /// </summary>
        /// <remarks>
        ///     O atributo <see cref="Identity" /> é imutável. Seguindo a lógica em que se
        ///     o identificador mudou, trata-se de uma nova <c>Entity</c>.
        /// </remarks>
        public TId Identity { get; }

        /// <summary>
        ///     Confere se a <c>Entity</c> está válida, tendo em conta suas
        ///     regras de validação.
        /// </summary>
        /// <returns><c>true</c> se a <c>Entity</c> for válida.</returns>
        public abstract bool IsValid();

        /// <summary>
        ///     Confere a igualdade de duas <c>Entity</c>.
        /// </summary>
        /// <remarks>
        ///     Duas <c>Entity</c> são comparadas por seus <see cref="Identity" />.
        /// </remarks>
        /// <param name="entity">A <c>Entity</c> a se comparar com esta.</param>
        /// <returns><c>true</c> se as <c>Entity</c> tiverem o mesmo <see cref="Identity" />.</returns>
        public bool Equals(Entity<TId> entity)
        {
            return ReferenceEquals(this, entity)
                   || !ReferenceEquals(null, entity)
                   && Identity.Equals(entity.Identity);
        }

        #region System.Object overrides

        /// <summary>
        ///     Confere a igualdade de duas <c>Entity</c>.
        /// </summary>
        /// <remarks>
        ///     Duas <c>Entity</c> são comparadas por seus <see cref="Identity" />.
        /// </remarks>
        /// <param name="obj">A <c>Entity</c> a se comparar com esta.</param>
        /// <returns><c>true</c> se as <c>Entity</c> tiverem o mesmo <see cref="Identity" />.</returns>
        public sealed override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                   || !ReferenceEquals(null, obj)
                   && obj is Entity<TId> entity
                   && Equals(entity);
        }

        /// <summary>
        ///     Confere a igualdade de duas <c>Entity</c>. Sobrescrevendo o operador <c>==</c>.
        /// </summary>
        /// <remarks>
        ///     Duas <c>Entity</c> são comparadas por seus <see cref="Identity" />.
        /// </remarks>
        /// <param name="a">A <c>Entity</c> a.</param>
        /// <param name="b">A <c>Entity</c> b.</param>
        /// <returns><c>true</c> se as <c>Entity</c> tiverem o mesmo <see cref="Identity" />.</returns>
        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            return ReferenceEquals(a, null) && ReferenceEquals(b, null)
                   || !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);
        }

        /// <summary>
        ///     Confere se duas <c>Entity</c> são diferentes. Sobrescrevendo o operador <c>!=</c>.
        /// </summary>
        /// <remarks>
        ///     Duas <c>Entity</c> são comparadas por seus <see cref="Identity" />.
        /// </remarks>
        /// <param name="a">A <c>Entity</c> a.</param>
        /// <param name="b">A <c>Entity</c> b.</param>
        /// <returns><c>true</c> se as <c>Entity</c> não tiverem o mesmo <see cref="Identity" />.</returns>
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
        ///     A representação da <c>Entity</c> em <c>string</c>.
        /// </summary>
        /// <returns>Sua representação em <c>string</c>.</returns>
        public override string ToString()
        {
            return $"{GetType().Name} [Id = {Identity}]";
        }

        #endregion
    }
}