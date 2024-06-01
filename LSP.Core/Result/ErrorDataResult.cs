namespace LSP.Core.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(T? data, string message, string messageCode) : base(false, message, messageCode, data)
        {
        }
    }
}
