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
    }
}