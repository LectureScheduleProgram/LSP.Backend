using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using LSP.Entity.Concrete;

namespace LSP.Business.Concrete
{
    public class SecurityHistoryManager : ISecurityHistoryService
    {
        private readonly ISecurityHistoryDal _securityHistoriesDal;

        public SecurityHistoryManager(ISecurityHistoryDal securityHistoriesDal)
        {
            _securityHistoriesDal = securityHistoriesDal;
        }

        public IDataResult<SecurityHistory> Add(SecurityHistory? userSecurityHistory)
        {
            if (userSecurityHistory == null)
                return new ErrorDataResult<SecurityHistory>(userSecurityHistory, Messages.add_failed,
                    Messages.add_failed_code);

            _securityHistoriesDal.Add(userSecurityHistory);
            return new SuccessDataResult<SecurityHistory>(userSecurityHistory, Messages.success,
                Messages.success_code);
        }

        public IDataResult<SecurityHistory> Delete(SecurityHistory securityHistory)
        {
            if (securityHistory == null)
                return new ErrorDataResult<SecurityHistory>(securityHistory, Messages.user_not_found,
                    Messages.user_not_found_code);

            _securityHistoriesDal.Delete(securityHistory);
            return new SuccessDataResult<SecurityHistory>(securityHistory, Messages.success,
                Messages.success);
        }

        public IDataResult<SecurityHistory> GetByFilter(Expression<Func<SecurityHistory, bool>> filter)
        {
            var result = _securityHistoriesDal.Get(filter);
            if (result != null)
                return new SuccessDataResult<SecurityHistory>(result, Messages.success, Messages.success_code);

            return new ErrorDataResult<SecurityHistory>(result, Messages.security_history_not_found,
                Messages.security_history_not_found_code);
        }

        public IDataResult<SecurityHistory?> GetById(int id)
        {
            var result = _securityHistoriesDal.Get(x => x.Id == id);
            if (result != null)
                return new SuccessDataResult<SecurityHistory>(result, Messages.success, Messages.success_code);

            return new ErrorDataResult<SecurityHistory>(result, Messages.security_history_not_found_code,
                Messages.security_history_not_found_code);
        }

        public IDataResult<List<SecurityHistory>> GetList()
        {
            var result = _securityHistoriesDal.GetList().ToList();
            if (result.Count != 0)
                return new SuccessDataResult<List<SecurityHistory>>(result, Messages.success,
                    Messages.success_code);

            return new ErrorDataResult<List<SecurityHistory>>(result, Messages.security_history_not_found,
                Messages.security_history_not_found_code);
        }

        public IDataResult<List<SecurityHistory>> GetListByFilter(
            Expression<Func<SecurityHistory, bool>> filter)
        {
            var result = _securityHistoriesDal.GetList(filter).ToList();
            if (result.Count != 0)
                return new SuccessDataResult<List<SecurityHistory>>(result, Messages.success,
                    Messages.success_code);

            return new ErrorDataResult<List<SecurityHistory>>(result, Messages.security_history_not_found,
                Messages.security_history_not_found_code);
        }

        public IDataResult<SecurityHistory> Update(SecurityHistory securityHistory)
        {
            if (securityHistory == null)
                return new ErrorDataResult<SecurityHistory>(securityHistory, Messages.update_failed,
                    Messages.update_failed_code);

            _securityHistoriesDal.Update(securityHistory);
            return new SuccessDataResult<SecurityHistory>(securityHistory, Messages.success,
                Messages.success_code);
        }
    }
}