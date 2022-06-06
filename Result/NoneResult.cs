namespace CC.Result
{
    /// <summary>
    ///     An IResult that represents nothing
    /// </summary>
    internal sealed class NoneResult : INoneResult { }

    /// <summary>
    ///     An IResult that represents nothing with the return type of <typeparamref name="T"/>
    /// </summary>
    internal sealed class NoneResult<T> : INoneResult<T> { }
}
