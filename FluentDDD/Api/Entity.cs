using System;
using System.Diagnostics.CodeAnalysis;

namespace FluentDDD.Api
{
    /// <summary>
    ///     Classe base para representar uma entidade de domínio.
    /// </summary>
    /// <remarks>
    ///     Uma <c>Entity</c> deve sempre possuir um identificador
    ///     do tipo <see cref="IEntityIdentity" />.
    ///     <para>
    ///         Diferente de um <see cref="ValueObject" />, uma <c>Entity</c> é comparada
    ///         por sua identidade, ou seja, se o valor de sua identidade for igual
    ///         à identidade de outra <c>Entity</c>, estas são consideradas iguais,
    ///         independente de seus outros atributos.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TId"></typeparam>
    [Serializable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public abstract class Entity<TId> where TId : IEntityIdentity
    {
        /// <summary>
        ///     Constrói uma <c>Entity</c> com seu <see cref="Identity" />.
        /// </summary>
        /// <seealso cref="IEntityIdentity" />
        /// <param name="identity">A identidade da <c>Entity</c>.</param>
        protected Entity(TId identity)
        {
            Identity = identity;
        }

        /// <summary>
        ///     O identificador <see cref="IEntityIdentity" /> da <c>Entity</c>.
        /// </summary>
        public TId Identity { get; }

        /// <summary>
        ///     Confere se a <c>Entity</c> está válida, tendo em conta suas
        ///     regras de validação.
        /// </summary>
        /// <returns><c>true</c> se a <c>Entity</c> for válida.</returns>
        public abstract bool IsValid();

        #region System.Object overrides

        /// <summary>
        ///     Confere a igualdade de duas <c>Entity</c>.
        /// </summary>
        /// <remarks>
        ///     Duas <c>Entity</c> são comparadas por seus <see cref="Identity" />.
        /// </remarks>
        /// <param name="obj">O <c>object</c> a ser comparado com a <c>Entity</c>.</param>
        /// <returns><c>true</c> se for considerado igual, ou <c>false</c> senão.</returns>
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<TId>;

            return ReferenceEquals(this, compareTo)
                   || !ReferenceEquals(null, compareTo)
                   && Identity.Equals(compareTo.Identity);
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
        public override int GetHashCode()
        {
            return GetType().GetHashCode() * new Random(100).Next() + Identity.GetHashCode();
        }

        /// <summary>
        ///     A representação da <c>Entity</c> em <c>string</c>.
        /// </summary>
        /// <returns>Sua representação em <c>string</c>.</returns>
        public override string ToString()
        {
            return $"{GetType().Name} [Identity = {Identity}]";
        }

        #endregion
    }
}