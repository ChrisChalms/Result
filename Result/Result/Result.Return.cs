namespace CC.Result
{
    /// <summary>
    ///     Helper methods for handling IResults
    /// </summary>
    public static partial class Result
    {
        /// <summary>
        ///     Wraps the supplied value in a <see cref="ISuccessResult{T}"/>. Returns <see cref="INoneResult" /> if the value is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IResult<T> Return<T>(T value) => value == null
            ? Return<T>()
            : new SuccessResult<T>(value);

        /// <summary>
        ///     Returns a <see cref="INoneResult"/>.
        /// </summary>
        /// <returns></returns>
        public static IResult Return() => new NoneResult();

        /// <summary>
        ///     Returns a <see cref="INoneResult{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IResult<T> Return<T>() => new NoneResult<T>();

        /// <summary>
        ///     Overload for Wrap that prevents double-wrapping an <see cref="IResult{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static IResult<T> Return<T>(IResult<T> value) => value;

        /// <summary>
        ///     Wraps an exception in an <see cref="IFailureResult"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IResult<T> Return<T, TException>(TException exception) where TException : Exception => new FailureResult<T>(exception);

        /// <summary>
        ///     Wraps an exception in an <see cref="IFailureResult"/> with the type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="failure"></param>
        /// <returns></returns>
        internal static IFailureResult<T> Return<T>(IFailureResult failure) => new FailureResult<T>(failure);
    }
}
