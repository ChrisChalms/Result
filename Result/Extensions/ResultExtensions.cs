namespace CC.Result
{
    public static class ResultExtensions
    {
        /// <summary>
        ///     Invokes a function on an <see cref="IResult{T}" /> that itself returns an <see cref="IResult{TOut}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut">The <see cref="IResult"/> to test</typeparam>
        /// <param name="result">The result</param>
        /// <param name="binder">A function that takes the value of type <typeparamref name="T" /> from a <see cref="IResult{T}" /> and transforms it into an option containing a value of type <typeparamref name="TOut" /></param>
        /// <returns></returns>
        public static IResult<TOut> Bind<T, TOut>(this IResult<T> result, Func<T, IResult<TOut>> binder) =>
            result switch
            {
                ISuccessResult<T> success => Result.Wrap(() => binder(success.Value)),
                IFailureResult failure => Result.Return<TOut>(failure),
                _ => Result.Return<TOut>()
            };

        /// <summary>
        ///     If the <paramref name="result" /> is <see cref="ISuccessResult" /> and the <paramref name="filter" /> predicate
        ///     returns true when passed the value in the <paramref name="result" />, this returns <see cref="ISuccessResult{T}" />.
        ///     Otherwise it returns a <see cref="INoneResult" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <param name="filter">A function that evaluates whether the value contained in the <paramref name="result" /> should remain, or be filtered out. /// </param>
        /// <returns></returns>
        public static IResult<T> Filter<T>(this IResult<T> result, Predicate<T> filter)
        {
            if (result is not ISuccessResult<T> success)
            {
                return result;
            }

            return Result.Wrap(() => filter(success.Value)) switch
            {
                ISuccessResult<bool> filterSuccess => filterSuccess.Value ? success : Result.Return<T>(),
                IFailureResult filterFailure => Result.Return<T>(filterFailure),
                _ => Result.Return<T>()
            };
        }

        /// <summary>
        ///     Returns the <paramref name="initialState" /> if the <paramref name="result" /> is <see cref="INoneResult" />; otherwise returns the updated state with the <paramref name="folder" /> and <paramref name="result" /> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut">The type of the out</typeparam>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <param name="folder">A function to update the state data when given a value from a <see cref="IResult{T}" /></param>
        /// <param name="initialState">The initial state.</param>
        /// <returns>The initial state if the <paramref name="result" /> is <see cref="INoneResult" />; otherwise returns the updated state with the <paramref name="folder" /> and <paramref name="result" /> value</returns>
        /// <exception cref="ResultException"><paramref name="folder" /> threw an exception</exception>
        public static TOut Fold<T, TOut>(this IResult<T> result, Func<TOut, T, TOut> folder, TOut initialState)
        {
            if (result is not ISuccessResult<T> success)
            {
                return initialState;
            }

            try
            {
                return folder(initialState, success.Value);
            }
            catch(Exception e)
            {
                throw new ResultException("Folder function threw an exception", e);
            }
        }

        /// <summary>
        ///     If the <paramref name="result" /> is <see cref="ISuccessResult{T}" />, then the <paramref name="action" /> is applied, otherwise no-op
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <param name="action">The action to apply to the <paramref name="result" /> value</param>
        /// <exception cref="ResultException"><paramref name="action" /> threw an exception</exception>
        public static void SuccessInvoke<T>(this IResult<T> result, Action<T> action)
        {
            if (result is not ISuccessResult<T> success)
            {
                return;
            }

            try
            {
                action?.Invoke(success.Value);
            }
            catch(Exception e)
            {
                throw new ResultException("SuccessInvoke action threw an exception", e);
            }
        }

        /// <summary>
        ///     If <paramref name="result" /> is <see cref="ISuccessResult" />, applies the given <paramref name="mapping" /> function to the value and returns a new <see cref="IResult{TOut}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut">The type of the out</typeparam>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <param name="mapping">The mapping</param>
        /// <returns></returns>
        public static IResult<TOut> Map<T, TOut>(this IResult<T> result, Func<T, TOut> mapping) =>
            result switch
            {
                ISuccessResult<T> success => Result.Wrap(() => mapping(success.Value)),
                IFailureResult failure => Result.Return<TOut>(failure),
                _ => Result.Return<TOut>()
            };

        /// <summary>
        ///     Determines whether this instance is an <see cref="ISuccessResult" />.
        /// </summary>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <returns><c>true</c> if the specified result is <see cref="ISuccessResult" />; otherwise, <c>false</c>.</returns>
        public static bool IsSuccess(this IResult result) => result is ISuccessResult;

        /// <summary>
        ///     Determines whether this instance is an <see cref="IFailureResult" />.
        /// </summary>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <returns><c>true</c> if the specified result is an <see cref="IFailureResult" />; otherwise, <c>false</c>.</returns>
        public static bool IsFailure(this IResult result) => result is IFailureResult;

        /// <summary>
        ///     Determines whether this instance is an <see cref="INoneResult" />.
        /// </summary>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <returns><c>true</c> if the specified result is an <see cref="INoneResult" />.; otherwise, <c>false</c>.</returns>
        public static bool IsNone(this IResult result) => result is INoneResult;

        public static int Count(this IResult result) => result.IsSuccess() ? 1 : 0;

        /// <summary>
        ///     Returns false if the <paramref name="result" /> is <see cref="INoneResult" />; otherwise returns the result of
        ///     applying the <paramref name="predicate" /> to the result value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The <see cref="IResult"/> to test</param>
        /// <param name="predicate">A predicate that accepts a value of type <typeparamref name="T" />.</param>
        /// <returns></returns>
        public static bool Exists<T>(this IResult<T> result, Predicate<T> predicate)
            => result.Filter(predicate).IsSuccess();
    }
}
