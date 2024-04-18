using LSP.Business.Abstract;
using LSP.Core.Entities.Concrete;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using LSP.Entity.DTO.User;
using System.Net;
using LSP.Core.Security;

namespace LSP.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _usersDal;

        public UserManager(IUserDal usersDal)
        {
            _usersDal = usersDal;
        }

        //İHTİYAÇ OLUNCA GERİ AÇILABİLİR  GETLİST
        //public ServiceResult<List<UserInformationDto>> GetList()
        //{
        //    var userListDto = new List<UserInformationDto>();
        //    var usersList = _usersDal.GetList().ToList();
        //    if (usersList==null)
        //    {
        //        return new ServiceResult<List<UserInformationDto>>
        //        {
        //            HttpStatusCode = (short)HttpStatusCode.NotFound,
        //            Result = new ErrorDataResult<List<UserInformationDto>>(new List<UserInformationDto>(),
        //           Messages.not_found_data,
        //           Messages.not_found_data_code)
        //        };
        //    }
        //    foreach (var user in usersList)
        //    {
        //        userListDto.Add(new UserInformationDto
        //        {
        //            Id = user.Id,
        //            Email = user.Email,
        //            Name = user.Name ?? "",
        //            Surname = user.Surname ?? "",
        //            Status = user.Status,
        //            PhoneNumber = user.PhoneNumber ?? "",
        //            CreatedDate = user.CreatedDate,
        //        });
        //    }
        //    return new ServiceResult<List<UserInformationDto>>
        //    {
        //        HttpStatusCode = (short)HttpStatusCode.OK,
        //        Result = new SuccessDataResult<List<UserInformationDto>>(userListDto,
        //        Messages.success,
        //        Messages.success_code)
        //    };
        //}

        public ServiceResult<bool> Update(User user)
        {
            var getUser = _usersDal.Get(x => x.Id == user.Id);
            if (getUser == null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.user_not_found,
                        Messages.user_not_found_code)
                };
            }

            _usersDal.Update(user);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<User> Get(Expression<Func<User, bool>> filter)
        {
            var result = _usersDal.Get(filter);
            if (result != null)
            {
                return new ServiceResult<User>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<User>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<User>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<User>(null,
                    Messages.user_not_found,
                    Messages.user_not_found_code)
            };
        }

        public ServiceResult<User> GetByMail(string email)
        {
            var result = _usersDal.Get(x => x.Email == email);
            if (result != null)
            {
                return new ServiceResult<User>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<User>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<User>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<User>(new User(),
                    Messages.not_found_data,
                    Messages.not_found_data_code)
            };
        }

        public ServiceResult<User> GetById(int id)
        {
            var user = _usersDal.Get(x => x.Id == id);
            if (user != null)
            {
                return new ServiceResult<User>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<User>(user,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<User>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<User>(new User(),
                    Messages.not_found_data,
                    Messages.not_found_data_code)
            };
        }

        public ServiceResult<bool> Add(User user)
        {
            _usersDal.Add(user);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> UpdateInformations(UpdateUserInformationDto userUpdateDto)
        {
            var userId = UserIdentityHelper.GetUserId();

            var user = _usersDal.Get(u => u.Id == userId);
            if (user == null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.not_found_data_code,
                        Messages.not_found_data_code)
                };
            }

            if (!string.IsNullOrEmpty(user.Name) && userUpdateDto.Name.Trim().Replace(" ", "") == user.Name.Trim().Replace(" ", ""))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.name_already_same,
                        Messages.name_already_same_code)
                };
            }

            if (!string.IsNullOrEmpty(user.Surname) && userUpdateDto.Surname.Trim().Replace(" ", "") == user.Surname.Trim().Replace(" ", ""))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.surname_already_same,
                        Messages.surname_already_same_code)
                };
            }

            if (user.PhoneNumber != null && (userUpdateDto.PhoneNumber == user.PhoneNumber))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.phone_number_already_same,
                        Messages.phone_number_already_same_code)
                };
            }

            var containsInvalidCharacter = userUpdateDto.PhoneNumber.Any(c => !char.IsDigit(c) && c != '+');

            if (containsInvalidCharacter)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.invalid_phone_number,
                        Messages.invalid_phone_number_code)
                };
            }


            user.Name = userUpdateDto.Name;
            user.Surname = userUpdateDto.Surname;
            user.PhoneNumber = userUpdateDto.PhoneNumber;

            _usersDal.Update(user);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<UserInformationDto> GetInformations()
        {
            var userId = UserIdentityHelper.GetUserId();
            var user = _usersDal.Get(x => x.Id == userId);
            if (user != null)
            {
                return new ServiceResult<UserInformationDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<UserInformationDto>(new UserInformationDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name ?? "",
                        Surname = user.Surname ?? "",
                        Status = user.Status,
                        PhoneNumber = user.PhoneNumber ?? "",
                        CreatedDate = user.CreatedDate
                    },
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<UserInformationDto>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<UserInformationDto>(new UserInformationDto(),
                    Messages.not_found_data,
                    Messages.not_found_data_code)
            };
        }
    }
}