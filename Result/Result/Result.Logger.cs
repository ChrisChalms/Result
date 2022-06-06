namespace CC.Result
{
    public static partial class Result
    {
        public static Action<Exception> Logger { get; private set; }

        public static void SetLogger(Action<Exception> action) => Logger = action;
    }
}
