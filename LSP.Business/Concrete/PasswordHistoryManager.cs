using System.Linq.Expressions;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using LSP.Entity.Concrete;

namespace LSP.Business.Concrete
{
    public class PasswordHistoryManager : IPasswordHistoryService
    {
        private IPasswordHistoryDal _passwordHistoriesDal;

        public PasswordHistoryManager(IPasswordHistoryDal passwordHistoriesDal)
        {
            _passwordHistoriesDal = passwordHistoriesDal;
        }

        public IDataResult<PasswordHistory> Add(PasswordHistory c)
        {
            if (c == null)
                return new ErrorDataResult<PasswordHistory>(null, Messages.add_failed, Messages.add_failed_code);

            _passwordHistoriesDal.Add(c);
            return new SuccessDataResult<PasswordHistory>(c, Messages.success, Messages.success_code);
        }

        public IDataResult<PasswordHistory> Del(PasswordHistory c)
        {
            if (c == null)
                return new ErrorDataResult<PasswordHistory>(null, Messages.delete_failed,
                    Messages.delete_failed_code);

            _passwordHistoriesDal.Delete(c);
            return new SuccessDataResult<PasswordHistory>(c, Messages.success, Messages.success_code);
        }

        public IDataResult<PasswordHistory> GetById(int id)
        {
            var result = _passwordHistoriesDal.Get(x => x.Id == id);
            if (result != null)
                return new SuccessDataResult<PasswordHistory>(result, Messages.success, Messages.success_code);

            return new ErrorDataResult<PasswordHistory>(null, Messages.password_history_not_found, Messages.password_history_not_found_code);
        }

        public IDataResult<List<PasswordHistory>> GetList(Expression<Func<PasswordHistory, bool>> filter = null)
        {
            var result = _passwordHistoriesDal.GetList(filter).ToList();
            if (result != null)
                return new SuccessDataResult<List<PasswordHistory>>(result, Messages.success,
                    Messages.success_code);

            return new ErrorDataResult<List<PasswordHistory>>(null, Messages.password_history_not_found,
                Messages.password_history_not_found_code);
        }

        public IDataResult<PasswordHistory> Update(PasswordHistory c)
        {
            if (c == null)
                return new ErrorDataResult<PasswordHistory>(null, Messages.update_failed,
                    Messages.update_failed_code);

            _passwordHistoriesDal.Update(c);
            return new SuccessDataResult<PasswordHistory>(c, Messages.success, Messages.success_code);
        }
    }
}