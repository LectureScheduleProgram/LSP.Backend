using LSP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Core.Pagination.Procedure
{
    public class PagingProcedureResponseDto<T> : IDto
        where T : class, IDto, new()
    {
        public List<T> Data { get; set; }
        public PagingDto Page { get; set; }
    }
}
