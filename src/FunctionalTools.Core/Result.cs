using System;
using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    public sealed class Result<TValue> : ResultBase
    {
        //--------------------------------------------------
        private Result(ResultState state, [NotNull] string tag, [CanBeNull] TValue value, Error error)
            : base(state, tag, error)
        {
            this._value = value;
        }

        public bool IsSuccess => this.State == ResultState.Success;

        public bool IsFailure => this.State == ResultState.Failure;

        //--------------------------------------------------
        public TValue Unwrap()
        {
            return this.State == ResultState.Success
                ? this._value
                : throw new InvalidOperationException(
                    string.IsNullOrEmpty(this.Tag)
                        ? Constants.ErrorMessages.FailureResultUnwrap
                        : $"{Constants.ErrorMessages.FailureResultUnwrap} Tag: {this.Tag}");
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TValue> Failure(Error error, [CanBeNull] string tag = null)
        {
            var validTag = tag.ToSafeString();
            return new Result<TValue>(ResultState.Failure, validTag, value: default, error);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TValue> Failure([CanBeNull] string message, [CanBeNull] string tag = null)
        {
            return Failure(new Error(message), tag);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TValue> Failure([NotNull] Exception exception, [CanBeNull] string tag = null)
        {
            return Failure(new Error(exception), tag);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TValue> Success([NotNull] TValue value, [CanBeNull] string tag = null)
        {
            var validTag = tag.ToSafeString();
            return new Result<TValue>(ResultState.Success, validTag, value, Error.None);
        }

        [CanBeNull] private readonly TValue _value;
    }
}