using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;

namespace LSP.Business.Concrete
{
    public class FacultyManager : IFacultyService
    {
        private readonly IFacultyDal _facultyDal;

        public FacultyManager(IFacultyDal FacultyDal)
        {
            _facultyDal = FacultyDal;
        }

        public ServiceResult<bool> Add(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.faculty_name_cant_empty,
                        Messages.faculty_name_cant_empty)
                };
            }

            _facultyDal.Add(new Faculty() { Name = name });
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(Faculty Faculty)
        {
            var getFaculty = _facultyDal.Get(x => x.Id == Faculty.Id);
            if (getFaculty is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.faculty_not_found,
                        Messages.faculty_not_found)
                };
            }

            _facultyDal.Update(Faculty);
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
            var Faculty = _facultyDal.Get(x => x.Id == id);
            if (Faculty is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.faculty_not_found,
                        Messages.faculty_not_found)
                };
            }

            _facultyDal.Delete(Faculty);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<Faculty> Get(Expression<Func<Faculty, bool>> filter)
        {
            var result = _facultyDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<Faculty>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Faculty>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Faculty>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Faculty>(null,
                    Messages.faculty_not_found,
                    Messages.faculty_not_found)
            };
        }

        public ServiceResult<Faculty> GetById(short id)
        {
            var Faculty = _facultyDal.Get(x => x.Id == id);
            if (Faculty is not null)
            {
                return new ServiceResult<Faculty>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Faculty>(Faculty,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Faculty>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Faculty>(new Faculty(),
                    Messages.faculty_not_found,
                    Messages.faculty_not_found)
            };
        }

        public ServiceResult<List<Faculty>> GetList()
        {
            var Facultys = _facultyDal.GetList();
            if (Facultys is not null)
            {
                return new ServiceResult<List<Faculty>>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<Faculty>>(Facultys.ToList(),
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<List<Faculty>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<Faculty>>(new List<Faculty>(),
                    Messages.faculty_not_found,
                    Messages.faculty_not_found)
            };
        }
    }
}