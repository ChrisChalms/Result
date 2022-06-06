namespace CC.Result
{
    /// <summary>
    ///     Represents errors that happen during <see cref="Result" /> operations
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class ResultException : Exception
    {
        /// <summary>
        /// Initialises a new <see cref="ResultException"/>
        /// </summary>
        public ResultException() { }

        /// <summary>
        /// Initialises a new <see cref="ResultException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public ResultException(string message) : base(message) { }

        /// <summary>
        /// Initialises a new <see cref="ResultException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">The exception that caused this <see cref="ResultException"/></param>
        public ResultException(string message, Exception innerException) : base(message, innerException) { }
    }
}
