using LSP.Core.Entities.Concrete;
using LSP.Core.Repository;
using LSP.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Dal.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        // List<OperationClaims> GetClaims(Users user);
    }
}
