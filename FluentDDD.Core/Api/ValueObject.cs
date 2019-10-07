using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

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
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private List<FieldInfo> _fields;
        private List<PropertyInfo> _properties;

        #region overrides

        /// <summary>
        ///     Checks if two <c>ValueObject</c>s are considerate equals. Overrides the <c>==</c> operator.
        /// </summary>
        /// <param name="vo1">The <c>ValueObject</c> one.</param>
        /// <param name="vo2">The <c>ValueObject</c> two.</param>
        /// <returns><c>true</c> if the attributes of both <c>ValueObject</c>s are equals.</returns>
        public static bool operator ==(ValueObject vo1, ValueObject vo2)
        {
            return vo1?.Equals(vo2) ?? Equals(vo2, null);
        }

        /// <summary>
        ///     Checks if two <c>ValueObject</c>s are <B>NOT</B> considerate equals. Overrides the <c>==</c> operator.
        /// </summary>
        /// <param name="vo1">The <c>ValueObject</c> one.</param>
        /// <param name="vo2">The <c>ValueObject</c> two.</param>
        /// <returns><c>true</c> if the attributes of both <c>ValueObject</c>s are <B>NOT</B> the same.</returns>
        public static bool operator !=(ValueObject vo1, ValueObject vo2)
        {
            return !(vo1 == vo2);
        }

        /// <inheritdoc />
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
        public bool Equals(ValueObject obj)
        {
            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                   && GetFields().All(f => FieldsAreEqual(obj, f));
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
        public override bool Equals(object obj)
        {
            return obj != null
                   && obj is ValueObject valueObject
                   && Equals(valueObject);
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            var hash = GetProperties()
                .Select(prop => prop.GetValue(this, null))
                .Aggregate(17, HashValue);

            return GetFields()
                .Select(field => field.GetValue(this))
                .Aggregate(hash, HashValue);
        }

        #endregion

        #region internal

        /// <summary>
        ///     Returns a Hash value of an <paramref name="value" />
        ///     considering a <paramref name="seed" />.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="value">The <c>object</c> of the Hash.</param>
        /// <returns></returns>
        private static int HashValue(int seed, object value)
        {
            return seed * 23 + value?.GetHashCode() ?? 0;
        }

        /// <summary>
        ///     Checks if the properties of this <c>object</c> are equals to given one.
        /// </summary>
        /// <param name="obj">The checked <c>object</c>.</param>
        /// <param name="p">The property info.</param>
        /// <returns></returns>
        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        /// <summary>
        ///     Checks if the fields of this <c>object</c> are equals of the given one.
        /// </summary>
        /// <param name="obj">The checked <c>object</c>.</param>
        /// <param name="f">The field info.</param>
        /// <returns></returns>
        private bool FieldsAreEqual(ValueObject obj, FieldInfo f)
        {
            return Equals(f.GetValue(this), f.GetValue(obj));
        }

        /// <summary>
        ///     Gets the properties of the <c>ValueObject</c> ignoring the ones marked as
        ///     <see cref="IgnoreMemberAttribute" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Just gets public and instance properties.
        ///     </para>
        /// </remarks>
        /// <returns>The list of properties of this <c>ValueObject</c>.</returns>
        private IEnumerable<PropertyInfo> GetProperties()
        {
            return _properties ?? (_properties = GetType()
                       .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       .Where(p => !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute)))
                       .ToList());
        }

        /// <summary>
        ///     Gets the fields of the <c>ValueObject</c> ignoring the ones marked as
        ///     <see cref="IgnoreMemberAttribute" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Gets public, non-public and instance fields.
        ///     </para>
        /// </remarks>
        /// <returns>The list of fields of this <c>ValueObject</c>.</returns>
        private IEnumerable<FieldInfo> GetFields()
        {
            return _fields ?? (_fields = GetType()
                       .GetFields(BindingFlags.Instance | BindingFlags.Public)
                       .Where(f => !Attribute.IsDefined(f, typeof(IgnoreMemberAttribute)))
                       .ToList());
        }

        #endregion
    }
}