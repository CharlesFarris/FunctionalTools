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
        public static Result<T> OnFailure<T>([NotNull] this Result<T> result, [NotNull] Action<Result<T>> action)
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
        public static Result<T> ToResult<T>([NotNull] this T value, [CanBeNull] string tag = null)
        {
            return Result<T>.Success(value, tag);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<T> ToResultNotNull<T>([CanBeNull] this T value, [CanBeNull] string tag = null)
            where T : class
        {
            return value is null
                ? Result<T>.Failure(tag)
                : Result<T>.Success(value, tag);
        }
    }
}