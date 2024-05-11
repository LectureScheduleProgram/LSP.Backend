using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;

namespace LSP.Business.Concrete
{
    public class ClassroomCapacityManager : IClassroomCapacityService
    {
        private readonly IClassroomCapacityDal _ClassroomCapacityDal;

        public ClassroomCapacityManager(IClassroomCapacityDal ClassroomCapacityDal)
        {
            _ClassroomCapacityDal = ClassroomCapacityDal;
        }

        public ServiceResult<bool> Add(ClassroomCapacity ClassroomCapacity)
        {
            _ClassroomCapacityDal.Add(ClassroomCapacity);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(ClassroomCapacity ClassroomCapacity)
        {
            var getClassroomCapacity = _ClassroomCapacityDal.Get(x => x.Id == ClassroomCapacity.Id);
            if (getClassroomCapacity is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroomCapacity_not_found,
                        Messages.classroomCapacity_not_found)
                };
            }

            _ClassroomCapacityDal.Update(ClassroomCapacity);
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
            var ClassroomCapacity = _ClassroomCapacityDal.Get(x => x.Id == id);
            if (ClassroomCapacity is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroomCapacity_not_found,
                        Messages.classroomCapacity_not_found)
                };
            }

            _ClassroomCapacityDal.Delete(ClassroomCapacity);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<ClassroomCapacity> Get(Expression<Func<ClassroomCapacity, bool>> filter)
        {
            var result = _ClassroomCapacityDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<ClassroomCapacity>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<ClassroomCapacity>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<ClassroomCapacity>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<ClassroomCapacity>(null,
                    Messages.classroomCapacity_not_found,
                    Messages.classroomCapacity_not_found)
            };
        }

        public ServiceResult<ClassroomCapacity> GetById(int id)
        {
            var ClassroomCapacity = _ClassroomCapacityDal.Get(x => x.Id == id);
            if (ClassroomCapacity is not null)
            {
                return new ServiceResult<ClassroomCapacity>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<ClassroomCapacity>(ClassroomCapacity,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<ClassroomCapacity>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<ClassroomCapacity>(new ClassroomCapacity(),
                    Messages.classroomCapacity_not_found,
                    Messages.classroomCapacity_not_found)
            };
        }

        public ServiceResult<List<ClassroomCapacity>> GetList()
        {
            var ClassroomCapacitys = _ClassroomCapacityDal.GetList();
            if (ClassroomCapacitys is not null)
            {
                return new ServiceResult<List<ClassroomCapacity>>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<ClassroomCapacity>>(ClassroomCapacitys.ToList(),
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<List<ClassroomCapacity>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<ClassroomCapacity>>(new List<ClassroomCapacity>(),
                    Messages.classroomCapacity_not_found,
                    Messages.classroomCapacity_not_found)
            };
        }
    }
}