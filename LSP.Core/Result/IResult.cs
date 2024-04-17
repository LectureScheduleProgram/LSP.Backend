using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Core.Result;

public interface IResult
{
    bool Success { get; }
    string Message { get; set; }
    string MessageCode { get; }
}