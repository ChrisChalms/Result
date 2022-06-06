namespace CC.Result
{
    /// <summary>
    /// An IResult that represents nothing
    /// </summary>
    public interface INoneResult : IResult { }

    /// <summary>
    /// An IResult that represents nothing with the return type of <typeparamref name="T"/>
    /// </summary>
    public interface INoneResult<T> : INoneResult, IResult<T> { }
}
