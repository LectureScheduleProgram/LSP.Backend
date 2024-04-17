using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSP.Core.Entities;

namespace LSP.Entity.DTO.UserSecurities
{
    public class UserSecuritiesDto : IDto
    {
        public int SecurityId { get; set; }
        public string SecurityType { get; set; }
        public byte Status { get; set; }
    }
}
