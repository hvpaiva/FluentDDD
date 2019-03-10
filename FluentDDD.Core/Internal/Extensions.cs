using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FluentDDD.Internal
{
    /// <summary>
    ///     Useful extensive functions.
    /// </summary>
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class Extensions
    {
        /// <summary>
        ///     Checks if the <c>object</c> is an <c>null</c> <c>object</c> and throw an exception.
        /// </summary>
        /// <param name="obj">The <c>object</c> to check.</param>
        /// <param name="paramName">The name of the param checked.</param>
        /// <param name="message">
        ///     The error message for the exception.
        ///     The default message is in constant <see cref="ErrorMessages"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the <c>object</c> checked is <c>null</c>.
        /// </exception>
        internal static void Guard(this object obj, string paramName, string message = ErrorMessages.NullOrEmptyError)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        ///     Checks if the <c>string</c> is an <c>null</c> or <B>empty</B> <c>object</c> and throw an exception.
        /// </summary>
        /// <param name="str">The <c>string</c> to check.</param>
        /// <param name="paramName">The name of the param checked.</param>
        /// <param name="message">
        ///     The error message for the exception.
        ///     The default message is in constant <see cref="ErrorMessages"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the <c>string</c> checked is <c>null</c> or <B>empty</B>.
        /// </exception>
        internal static void Guard(this string str, string paramName, string message = ErrorMessages.NullOrEmptyError)
        {
            if (str == null)
                throw new ArgumentNullException(paramName, string.Format(message, paramName));

            if (string.IsNullOrEmpty(str))
                throw new ArgumentException(string.Format(message, paramName), paramName);
        }

        /// <summary>
        ///     Splits pascal case.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         So the "FooBar" would be "Foo Bar".
        ///     </para>
        /// </remarks>
        /// <param name="input">The string to be splitter.</param>
        /// <returns>The splitter <paramref name="input"/>.</returns>
        public static string SplitPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var stringBuilder = new StringBuilder(input.Length + 5);

            for (var index = 0; index < input.Length; ++index)
            {
                var c = input[index];

                if (char.IsUpper(c) && (index > 1 && !char.IsUpper(input[index - 1])
                                        || index + 1 < input.Length
                                        && !char.IsUpper(input[index + 1])))
                {
                    stringBuilder.Append(' ');
                }

                stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Trim();
        }

        /// <summary>
        ///     In line foreach function.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <param name="action">The <see cref="Action{T}"/>.</param>
        /// <typeparam name="T">The type in the <see cref="IEnumerable{T}"/>.</typeparam>
        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var obj in source)
                action(obj);
        }

        /// <summary>
        ///     Searches from an <see cref="IDictionary{TKey,TValue}"/> and gets a
        ///     an value or adds if its not exists.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type object.</typeparam>
        /// <returns></returns>
        internal static T GetOrAdd<T>(this IDictionary<string, object> dict, string key, Func<T> value)
        {
            if (dict.TryGetValue(key, out var obj1) && obj1 is T obj2)
                return obj2;

            var obj3 = value();
            dict[key] = (object) obj3;

            return obj3;
        }
    }
}