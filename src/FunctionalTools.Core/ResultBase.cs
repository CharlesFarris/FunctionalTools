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

    public abstract class ResultBase
    {
        //--------------------------------------------------
        protected ResultBase(ResultState state, [NotNull] string tag)
        {
            this.State = state;
            this.Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        public ResultState State { get; }

        [NotNull] public string Tag { get; }
    }
}