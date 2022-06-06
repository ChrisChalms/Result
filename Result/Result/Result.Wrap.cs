namespace CC.Result
{
    public static partial class Result
    {
        /// <summary>
        ///     Executes the <paramref name="action"/> and return the result in an <see cref="ISuccessResult{T}"/>
        ///     Returns an <see cref="INoneResult"/> if the value is null, or an <see cref="IFailureResult"/> if an exception is thrown
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exceptionHandler"></param>
        /// <returns></returns>
        public static IResult<bool> Wrap(Action action, Func<Exception, Exception> exceptionHandler = null)
        {
            try
            {
                action();
                return Return(true);
            }
            catch(Exception e)
            {
                return Wrap<bool>(exceptionHandler, e);
            }
        }

        /// <summary>
        ///     Executes the <paramref name="function"/> and return the result in an <see cref="ISuccessResult{T}"/>
        ///     Returns an <see cref="INoneResult"/> if the value is null, or an <see cref="IFailureResult"/> if an exception is thrown
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <param name="exceptionHandler"></param>
        /// <returns></returns>
        public static IResult<T> Wrap<T>(Func<T> function, Func<Exception, Exception> exceptionHandler = null)
        {
            try
            {
                return Return(function());
            }
            catch (Exception e)
            {
                return Wrap<T>(exceptionHandler, e);
            }
        }

        internal static IResult<T> Wrap<T>(Func<IResult<T>> function, Func<Exception, Exception> exceptionHandler = null)
        {
            try
            {
                return Return(function());
            }
            catch(Exception e)
            {
                return Wrap<T>(exceptionHandler, e);
            }
        }

        static IFailureResult<T> Wrap<T>(Func<Exception, Exception> exceptionHandler, Exception exception)
        {
            var handler = exceptionHandler ?? (e => e);
            try
            {
                return (IFailureResult<T>)Return<T, Exception>(handler(exception));
            }
            catch(Exception e)
            {
                return (IFailureResult<T>)Return<T, Exception>(new ResultException("Exception Handler threw an exception!", e));
            }
        }
    }
}
