using System.Linq.Expressions;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using LSP.Entity.Concrete;

namespace LSP.Business.Concrete
{
    public class UserStatusHistoryManager : IUserStatusHistoryService
    {
        private readonly IUserStatusHistoryDal _userStatusHistoriesDal;

        public UserStatusHistoryManager(IUserStatusHistoryDal userStatusHistoriesDal)
        {
            _userStatusHistoriesDal = userStatusHistoriesDal;
        }

        public IDataResult<UserStatusHistory> Add(UserStatusHistory c)
        {
            if (c == null)
                return new ErrorDataResult<UserStatusHistory>(null, Messages.add_failed_code, Messages.add_failed);

            _userStatusHistoriesDal.Add(c);
            return new SuccessDataResult<UserStatusHistory>(c, Messages.success, Messages.success);

        }

        public IDataResult<UserStatusHistory> Del(UserStatusHistory c)
        {
            if (c == null)
                return new ErrorDataResult<UserStatusHistory>(null, Messages.delete_failed, Messages.delete_failed_code);

            _userStatusHistoriesDal.Delete(c);
            return new SuccessDataResult<UserStatusHistory>(c, Messages.success, Messages.success);

        }

        public IDataResult<UserStatusHistory> GetById(int id)
        {
            var result = _userStatusHistoriesDal.Get(x => x.Id == id);
            if (result != null)
                return new SuccessDataResult<UserStatusHistory>(result, Messages.success, Messages.success);

            return new ErrorDataResult<UserStatusHistory>(null, Messages.user_status_not_found, Messages.user_status_not_found_code);
        }

        public IDataResult<List<UserStatusHistory>> GetList()
        {
            var result = _userStatusHistoriesDal.GetList().ToList();
            if (result.Count == 0)
                return new SuccessDataResult<List<UserStatusHistory>>(result, Messages.success, Messages.success);

            return new ErrorDataResult<List<UserStatusHistory>>(result, Messages.user_status_not_found,
                Messages.user_status_not_found_code);
        }

        public IDataResult<List<UserStatusHistory>> GetListByFilter(Expression<Func<UserStatusHistory, bool>> filter)
        {
            var result = _userStatusHistoriesDal.GetList(filter).ToList();
            if (result.Count == 0)
                return new SuccessDataResult<List<UserStatusHistory>>(result, Messages.success, Messages.success);

            return new ErrorDataResult<List<UserStatusHistory>>(result, Messages.user_status_not_found,
                Messages.user_status_not_found_code);
        }

        public IDataResult<UserStatusHistory> GetByFilter(Expression<Func<UserStatusHistory, bool>> filter)
        {
            var result = _userStatusHistoriesDal.Get(filter);
            if (result != null)
                return new SuccessDataResult<UserStatusHistory>(result, Messages.success, Messages.success);

            return new ErrorDataResult<UserStatusHistory>(result, Messages.user_status_not_found,
                Messages.user_status_not_found_code);
        }

        public IDataResult<UserStatusHistory> Update(UserStatusHistory c)
        {
            if (c == null)
                return new ErrorDataResult<UserStatusHistory>(null, Messages.update_failed_code, Messages.update_failed);

            _userStatusHistoriesDal.Update(c);
            return new SuccessDataResult<UserStatusHistory>(c, Messages.success, Messages.success);
        }
    }
}