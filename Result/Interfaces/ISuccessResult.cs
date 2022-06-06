namespace CC.Result
{
    /// <summary>
    /// A successful result without a return value
    /// </summary>
    public interface ISuccessResult : IResult { }

    /// <summary>
    /// A successful result with the return type of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISuccessResult<T> : ISuccessResult, IResult<T>
    {
        T Value { get; }
    }
}
