using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    public sealed class Result<T> : ResultBase
    {
        private Result(ResultState state)
            : base(state)
        {
        }

        [NotNull] public static Result<T> Failure()
        {
            return new Result<T>(ResultState.Failure);
        }

        [NotNull] public static Result<T> Success()
        {
            return new Result<T>(ResultState.Success);
        }
        
        [NotNull] public static Result<T> ToResult(T value)
        {
            return Result<T>.Success();
        }

    }
}