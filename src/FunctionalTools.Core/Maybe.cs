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
        private Maybe([CanBeNull] T value)
        {
            this._value = value;
        }

        public bool HasNoValue => this._value is null;

        public bool HasValue => !this.HasNoValue;

        [NotNull]
        // ReSharper disable once AssignNullToNotNullAttribute
        public T Value => this.HasNoValue
            ? throw new InvalidOperationException(Constants.ErrorMessages.MaybeHasNoValue)
            : this._value;

        public static readonly Maybe<T> Empty = new Maybe<T>(default);

        //--------------------------------------------------
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