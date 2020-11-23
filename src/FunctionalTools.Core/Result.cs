using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    public sealed class Result<T> : ResultBase
    {
        //--------------------------------------------------
        private Result(ResultState state, [NotNull] string tag)
            : base(state, tag)
        {
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<T> Failure([CanBeNull] string tag = null)
        {
            var validTag = tag.ToSafeString();
            return new Result<T>(ResultState.Failure, validTag);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<T> Success([CanBeNull] string tag = null)
        {
            var validTag = tag.ToSafeString();
            return new Result<T>(ResultState.Success, validTag);
        }

        //--------------------------------------------------
        [NotNull]
        public static Result<T> ToResult(T value, [CanBeNull] string tag = null)
        {
            return Result<T>.Success(tag);
        }
    }
}