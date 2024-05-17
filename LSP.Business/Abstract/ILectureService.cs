using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface ILectureService
    {
        ServiceResult<bool> Add(string name);
        ServiceResult<bool> Update(Lecture Lecture);
        ServiceResult<bool> Delete(int id);
        ServiceResult<Lecture> GetById(int id);
        ServiceResult<List<Lecture>> GetList();
        ServiceResult<Lecture> Get(Expression<Func<Lecture, bool>> filter);
    }
}