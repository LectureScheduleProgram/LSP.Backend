using System.Linq.Expressions;
using LSP.Core.Result;
using LSP.Entity.Concrete;
namespace LSP.Business.Abstract
{
    public interface IPasswordHistoryService
    {
        IDataResult<PasswordHistory> GetById(int id);
        IDataResult<List<PasswordHistory>> GetList(Expression<Func<PasswordHistory, bool>> filter = null);
        IDataResult<PasswordHistory> Add(PasswordHistory passwordHistories);
        IDataResult<PasswordHistory> Del(PasswordHistory passwordHistories);
        IDataResult<PasswordHistory> Update(PasswordHistory passwordHistories);
    }
}
