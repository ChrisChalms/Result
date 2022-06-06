namespace CC.Result
{
    /// <summary>
    /// A failed result with an exception
    /// </summary>
    public interface IFailureResult : INoneResult
    {
        /// <summary>
        /// The Exception that caused the failure
        /// </summary>
        Exception Exception { get; }
    }

    /// <summary>
    /// A failed result with an expected return value of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IFailureResult<T> : IFailureResult, INoneResult<T> { }
}
