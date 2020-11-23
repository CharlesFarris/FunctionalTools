using System;
using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    public sealed class Result<T> : ResultBase
    {
        //--------------------------------------------------
        private Result(ResultState state, [NotNull] string tag, [CanBeNull] T value)
            : base(state, tag)
        {
            this._value = value;
        }

        //--------------------------------------------------
        public T Unwrap()
        {
            return this.State == ResultState.Success
                ? this._value
                : throw new InvalidOperationException(
                    string.IsNullOrEmpty(this.Tag)
                        ? Constants.Result.FailureResultUnwrap
                        : $"{Constants.Result.FailureResultUnwrap} Tag: {this.Tag}");
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<T> Failure([CanBeNull] string tag = null)
        {
            var validTag = tag.ToSafeString();
            return new Result<T>(ResultState.Failure, validTag, value: default);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<T> Success([NotNull] T value, [CanBeNull] string tag = null)
        {
            var validTag = tag.ToSafeString();
            return new Result<T>(ResultState.Success, validTag, value);
        }
        
        [CanBeNull] private readonly T _value;
    }
}