
using LSP.Core.Entities;

namespace LSP.Core.Result;

public class ServiceResult<T> : IDto
{
    public short HttpStatusCode { get; set; }
    public IDataResult<T> Result { get; set; }
}