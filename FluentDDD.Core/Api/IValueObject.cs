namespace FluentDDD.Api
{
    /// <summary>
    ///     Represents an Value Object.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This interface should not be implemented directly in your code.
    ///         Please inherit from <see cref="ValueObject"/> instead.
    ///     </para>
    ///     <para>
    ///         An <c>IValueObject</c> is always immutable and should
    ///         give an way to copy itself.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TValueObject">The <see cref="ValueObject"/>.</typeparam>
    internal interface IValueObject<out TValueObject> where TValueObject : IValueObject<TValueObject>
    {
        /// <summary>
        ///     Creates a copy of this <c>IValueObject</c>.
        /// </summary>
        /// <returns>A copy of itself.</returns>
        TValueObject Copy();
    }
}