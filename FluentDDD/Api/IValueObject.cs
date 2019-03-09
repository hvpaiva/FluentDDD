namespace FluentDDD.Api
{
    /// <summary>
    ///     Representação de um Value Objects.
    /// </summary>
    /// <remarks>
    ///     Um <c>IValueObject</c> é sempre imutável, e deve saber
    ///     fornecer uma cópia de si mesmo.
    /// </remarks>
    public interface IValueObject
    {
        /// <summary>
        ///     Cria um clone do <c>IValueObject</c>.
        /// </summary>
        /// <returns>O clone deste <c>IValueObject</c>.</returns>
        IValueObject Copy();
    }
}