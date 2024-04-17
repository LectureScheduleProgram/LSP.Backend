using LSP.Core.Entities.Concrete;
using LSP.Core.Result;
using System.Linq.Expressions;
using LSP.Entity.DTO.User;
using LSP.Entity.DTO.Parity;

namespace LSP.Business.Abstract
{
    public interface IUserService
    {
        ServiceResult<User> GetById(int id);
        ServiceResult<UserInformationDto> GetInformations();
        //ServiceResult<List<UserInformationDto>> GetList();
        ServiceResult<bool> Update(User user);
        ServiceResult<bool> UpdateInformations(UpdateUserInformationDto userUpdateDto);
        ServiceResult<bool> Add(User user);
        ServiceResult<User> Get(Expression<Func<User, bool>> filter);
        ServiceResult<User> GetByMail(string email);


    }
}