namespace FluentDDD.Api
{
    /// <summary>
    ///     Represents an Value Object.
    /// </summary>
    /// <remarks>
    ///     An <c>IValueObject</c> is always immutable and should
    ///     give an way to copy itself.
    /// </remarks>
    public interface IValueObject
    {
        /// <summary>
        ///     Creates a copy of this <c>IValueObject</c>.
        /// </summary>
        /// <returns>A copy of itself.</returns>
        IValueObject Copy();
    }
}