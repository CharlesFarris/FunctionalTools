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
        public ResultState State { get; }

        protected ResultBase(ResultState state)
        {
            this.State = state;
        }
    }
}