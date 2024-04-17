using System.Linq.Expressions;
using LSP.Core.Result;
using LSP.Entity.Concrete;
namespace LSP.Business.Abstract
{
    public interface ISecurityHistoryService
    {
        IDataResult<SecurityHistory> GetById(int id);
        IDataResult<SecurityHistory> GetByFilter(Expression<Func<SecurityHistory, bool>> filter);
        IDataResult<List<SecurityHistory>> GetList();
        IDataResult<List<SecurityHistory>> GetListByFilter(Expression<Func<SecurityHistory, bool>> filter);
        IDataResult<SecurityHistory> Add(SecurityHistory securityHistory);
        IDataResult<SecurityHistory> Delete(SecurityHistory securityHistories);
        IDataResult<SecurityHistory> Update(SecurityHistory securityHistory);
    }
}
