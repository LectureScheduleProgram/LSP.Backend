using System.Linq.Expressions;
using LSP.Core.Result;
using LSP.Entity.Concrete;
namespace LSP.Business.Abstract
{
    public interface IUserStatusHistoryService
    {
        IDataResult<UserStatusHistory> GetById(int id);
        IDataResult<List<UserStatusHistory>> GetList();
        IDataResult<List<UserStatusHistory>> GetListByFilter(Expression<Func<UserStatusHistory, bool>> filter);
        IDataResult<UserStatusHistory> GetByFilter(Expression<Func<UserStatusHistory, bool>> filter);
        IDataResult<UserStatusHistory> Add(UserStatusHistory c);
        IDataResult<UserStatusHistory> Del(UserStatusHistory c);
        IDataResult<UserStatusHistory> Update(UserStatusHistory c);
    }
}
