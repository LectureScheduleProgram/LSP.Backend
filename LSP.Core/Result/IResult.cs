namespace LSP.Core.Result;

public interface IResult
{
    bool Success { get; }
    string Message { get; set; }
    string MessageCode { get; }
}