namespace CC.Result
{
    /// <summary>
    ///     A successful result with the return type of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class SuccessResult<T> : ISuccessResult<T>
    {
        public T Value { get; }

        public SuccessResult(T value) => Value = value;
    }
}
