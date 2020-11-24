using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    /// <summary>
    /// Struct for simplifying null value handling.
    /// <remarks>
    /// Inspired by V. Khorikov's example in the "Applying Functional Principles in C#".
    /// </remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct Maybe<T> : IEquatable<Maybe<T>> where T : class
    {
        //--------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">The value being wrapped.</param>
        private Maybe([CanBeNull] T value)
        {
            this._value = value;
        }

        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool HasNoValue => this._value is null;

        /// <summary>
        /// Returns true if the value is not null.
        /// </summary>
        public bool HasValue => !this.HasNoValue;

        /// <summary>
        /// Gets the value. If the value is null, an <see cref="InvalidOperationException"/>
        /// is thrown.
        /// </summary>
        [NotNull]
        // ReSharper disable once AssignNullToNotNullAttribute
        public T Value => this.HasNoValue
            ? throw new InvalidOperationException(Constants.ErrorMessages.MaybeHasNoValue)
            : this._value;

        /// <summary>
        /// Static empty instance.
        /// </summary>
        public static readonly Maybe<T> Empty = new Maybe<T>(default);

        //--------------------------------------------------
        /// <summary>
        /// Implements the <see cref="IEquatable{T}"/> interface.
        /// </summary>
        /// <param name="other">Instance to be compared.</param>
        /// <returns></returns>
        public bool Equals(Maybe<T> other)
        {
            if (this.HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (this.HasNoValue || other.HasNoValue)
            {
                return false;
            }

            // ReSharper disable once PossibleNullReferenceException
            return this._value.Equals(other._value);
        }

        //--------------------------------------------------
        public override bool Equals(object obj)
        {
            return obj is Maybe<T> other && this.Equals(other);
        }

        //--------------------------------------------------
        public override int GetHashCode()
        {
            return this.HasNoValue
                ? 0
                // ReSharper disable once AssignNullToNotNullAttribute
                : EqualityComparer<T>.Default.GetHashCode(this._value);
        }

        //--------------------------------------------------
        public override string ToString()
        {
            return this.HasNoValue
                ? "No Value"
                // ReSharper disable once PossibleNullReferenceException
                : this._value.ToString();
        }

        //--------------------------------------------------
        [CanBeNull]
        public T Unwrap([CanBeNull] T defaultValue = default)
        {
            return this.HasNoValue
                ? defaultValue
                : this._value;
        }

        //--------------------------------------------------
        /// <summary>
        /// Factory method for creating a <see cref="Maybe{T}"/>
        /// instance.
        /// </summary>
        /// <param name="value">Value being wrapped.</param>
        /// <returns></returns>
        public static Maybe<T> From([CanBeNull] T value)
        {
            return new Maybe<T>(value);
        }

        //--------------------------------------------------
        public static bool operator ==(Maybe<T> maybe, T value)
        {
            return maybe.HasValue && maybe.Value.Equals(value);
        }

        //--------------------------------------------------
        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        //--------------------------------------------------
        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        //--------------------------------------------------
        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        [CanBeNull] private readonly T _value;
    }
}