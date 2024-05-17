namespace LSP.Core.Result
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(T? data, string message, string messageCode) : base(true, message, messageCode, data)
        {
        }
    }
}
