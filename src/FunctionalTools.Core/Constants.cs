using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    public static class Constants
    {
        public static class ErrorMessages
        {
            [NotNull] public const string FailureResultUnwrap = "State is failure.";

            [NotNull] public const string MaybeHasNoValueUnWrap = "Has no value.";
        }
        
    }
}