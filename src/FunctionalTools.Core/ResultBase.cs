using System;
using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    public enum ResultState
    {
        Unknown = 0,
        Success = 1,
        Failure = 2
    }

    /// <summary>
    /// Abstract base type used by concrete result class implementations.
    /// </summary>
    public abstract class ResultBase
    {
        //--------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        protected ResultBase(ResultState state, [NotNull] string tag, Error error)
        {
            if (state == ResultState.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(state));
            }

            this.State = state;
            this.Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            this.Error = error;
        }

        public ResultState State { get; }

        [NotNull] public string Tag { get; }

        public Error Error { get; }
    }
}