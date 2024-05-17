using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;
using LSP.Entity.Enum.Classroom;
using LSP.Core.Extensions;

namespace LSP.Business.Concrete
{
    public class ClassroomTypeManager : IClassroomTypeService
    {
        private readonly IClassroomTypeDal _ClassroomTypeDal;

        public ClassroomTypeManager(IClassroomTypeDal ClassroomTypeDal)
        {
            _ClassroomTypeDal = ClassroomTypeDal;
        }

        public ServiceResult<bool> Add(ClassroomTypeEnum type)
        {
            _ClassroomTypeDal.Add(new ClassroomType() { Name = type.EnumToString() });
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(ClassroomType ClassroomType)
        {
            var getClassroomType = _ClassroomTypeDal.Get(x => x.Id == ClassroomType.Id);
            if (getClassroomType is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroomType_not_found,
                        Messages.classroomType_not_found)
                };
            }

            _ClassroomTypeDal.Update(ClassroomType);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Delete(int id)
        {
            var ClassroomType = _ClassroomTypeDal.Get(x => x.Id == id);
            if (ClassroomType is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroomType_not_found,
                        Messages.classroomType_not_found)
                };
            }

            _ClassroomTypeDal.Delete(ClassroomType);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<ClassroomType> Get(Expression<Func<ClassroomType, bool>> filter)
        {
            var result = _ClassroomTypeDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<ClassroomType>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<ClassroomType>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<ClassroomType>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<ClassroomType>(null,
                    Messages.classroomType_not_found,
                    Messages.classroomType_not_found)
            };
        }

        public ServiceResult<ClassroomType> GetById(int id)
        {
            var ClassroomType = _ClassroomTypeDal.Get(x => x.Id == id);
            if (ClassroomType is not null)
            {
                return new ServiceResult<ClassroomType>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<ClassroomType>(ClassroomType,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<ClassroomType>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<ClassroomType>(new ClassroomType(),
                    Messages.classroomType_not_found,
                    Messages.classroomType_not_found)
            };
        }

        public ServiceResult<List<ClassroomType>> GetList()
        {
            var ClassroomTypes = _ClassroomTypeDal.GetList();
            if (ClassroomTypes is not null)
            {
                return new ServiceResult<List<ClassroomType>>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<ClassroomType>>(ClassroomTypes.ToList(),
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<List<ClassroomType>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<ClassroomType>>(new List<ClassroomType>(),
                    Messages.classroomType_not_found,
                    Messages.classroomType_not_found)
            };
        }
    }
}