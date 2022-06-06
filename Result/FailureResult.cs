namespace CC.Result
{
    internal sealed class FailureResult<T> : FailureResult, IFailureResult<T>
    {
        public FailureResult(Exception exception) : base(exception) { }
        public FailureResult(IFailureResult failure) : base(failure) { }
    }

    internal class FailureResult : IFailureResult
    {
        public Exception Exception { get; }

        /// <summary>
        ///     Initialises a new <see cref="FailureResult"/> and uses the logger in <see cref="Result"/> to log exceptions
        /// </summary>
        /// <param name="exception"></param>
        public FailureResult(Exception exception)
        {
            Exception = exception ?? new Exception("Exception passed");
            Result.Logger(Exception);
        }
        
        protected FailureResult(IFailureResult failure) => Exception = failure.Exception;
    }
}
