namespace FluentDDD.Internal
{
    /// <summary>
    ///     Supports cloning, which creates a new instance of a class with
    ///     the same value as an existing instance.
    /// </summary>
    /// <typeparam name="TType">The type of the cloned object.</typeparam>
    internal interface ICloneable<out TType>
    {
        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>The clone of the current instance.</returns>
        TType Clone();
    }
}