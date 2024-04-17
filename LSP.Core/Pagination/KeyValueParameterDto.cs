using LSP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Core.Pagination
{
    public class KeyValueParameterDto : IDto
    {
        public string Name { get; set; }
        public int[] Values { get; set; }
    }
}
