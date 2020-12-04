using System;
using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    /// <summary>
    /// Extension methods for the <see cref="Result{T}"/> class.
    /// </summary>
    public static class ResultExtensions
    {
        //--------------------------------------------------
        [NotNull]
        public static Result<TInput> OnFailure<TInput>(
            [NotNull] this Result<TInput> result,
            [NotNull] Action<Result<TInput>> action)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (result.State == ResultState.Failure)
            {
                action(result);
            }

            return result;
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TInput> OnSuccess<TInput>(
            [NotNull] this Result<TInput> result,
            [NotNull] Action<TInput> action)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (result.State)
            {
                case ResultState.Failure:
                    return result;

                case ResultState.Success:
                    action.Invoke(result.Unwrap());
                    return result;

                default:
                    throw new InvalidOperationException(Constants.ErrorMessages.InvalidResultState);
            }
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TOutput> OnSuccess<TInput, TOutput>(
            [NotNull] this Result<TInput> result,
            [NotNull] Func<TInput, Result<TOutput>> func)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (result.State)
            {
                case ResultState.Failure:
                    return Result<TOutput>.Failure(result.Tag);

                case ResultState.Success:
                    return func.Invoke(result.Unwrap());

                default:
                    throw new InvalidOperationException(Constants.ErrorMessages.InvalidResultState);
            }
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TInput> ToResult<TInput>([NotNull] this TInput value, [CanBeNull] string tag = null)
        {
            return Result<TInput>.Success(value, tag);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<TInput> ToResultNotNull<TInput>(
            [CanBeNull] this TInput value,
            [CanBeNull] string failureMessage = null,
            [CanBeNull] string tag = null)
            where TInput : class
        {
            var validFailureMessage = string.IsNullOrEmpty(failureMessage)
                ? "Value cannot be null."
                : failureMessage;
            return value is null
                ? Result<TInput>.Failure(validFailureMessage, tag)
                : Result<TInput>.Success(value, tag);
        }
    }
}