using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Department;

namespace LSP.Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;
        private readonly IFacultyService _facultyService;

        public DepartmentManager(IDepartmentDal DepartmentDal, IFacultyService facultyService)
        {
            _departmentDal = DepartmentDal;
            _facultyService = facultyService;
        }

        public ServiceResult<bool> Add(AddDepartmentDto department)
        {
            if (string.IsNullOrEmpty(department.Name))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.department_name_cant_empty,
                        Messages.department_name_cant_empty)
                };
            }

            var faculty = _facultyService.GetById(department.FacultyId);
            if (!faculty.Result.Success)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.faculty_not_found,
                        Messages.faculty_not_found)
                };
            }

            _departmentDal.Add(new Department()
            {
                Name = department.Name,
                FacultyId = department.FacultyId
            });

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(UpdateDepartmentDto request)
        {
            var departmentFromDb = _departmentDal.Get(x => x.Id == request.Id);
            if (departmentFromDb is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.department_not_found,
                        Messages.department_not_found)
                };
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                var departmentIsExists = _departmentDal.Get(x => x.Name == request.Name.Trim());
                if (departmentIsExists is not null)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.BadRequest,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.department_already_exists,
                            Messages.department_already_exists)
                    };
                }

                departmentFromDb.Name = request.Name;
            }

            if (request.FacultyId is not 0 && request.FacultyId > 0)
            {
                var faculty = _facultyService.GetById((short)request.FacultyId!);
                if (!faculty.Result.Success)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.NotFound,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.faculty_not_found,
                            Messages.faculty_not_found)
                    };
                }

                departmentFromDb.FacultyId = (short)request.FacultyId!;
            }

            departmentFromDb.UpdatedDate = DateTime.Now;
            _departmentDal.Update(departmentFromDb);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Delete(short id)
        {
            var departmentFromDb = _departmentDal.Get(x => x.Id == id);
            if (departmentFromDb is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.department_not_found,
                        Messages.department_not_found)
                };
            }

            _departmentDal.Delete(departmentFromDb);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<Department> Get(Expression<Func<Department, bool>> filter)
        {
            var result = _departmentDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<Department>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Department>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Department>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Department>(null,
                    Messages.department_not_found,
                    Messages.department_not_found)
            };
        }

        public ServiceResult<DepartmentDto> GetById(short id)
        {
            var department = _departmentDal.Get(x => x.Id == id);
            if (department is not null)
            {
                var faculty = _facultyService.GetById(department.FacultyId).Result;
                if (!faculty.Success)
                {
                    return new ServiceResult<DepartmentDto>
                    {
                        HttpStatusCode = (short)HttpStatusCode.NotFound,
                        Result = new ErrorDataResult<DepartmentDto>(null,
                            Messages.faculty_not_found,
                            Messages.faculty_not_found)
                    };
                }

                var departmentDto = new DepartmentDto()
                {
                    Name = department.Name,
                    FacultyName = faculty.Data!.Name,
                    CreatedDate = department.CreatedDate,
                    UpdatedDate = department.UpdatedDate
                };

                return new ServiceResult<DepartmentDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<DepartmentDto>(departmentDto,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<DepartmentDto>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<DepartmentDto>(null,
                    Messages.department_not_found,
                    Messages.department_not_found)
            };
        }

        public ServiceResult<List<DepartmentDto>> GetList()
        {
            var departments = _departmentDal.GetList();
            var departmentDtos = new List<DepartmentDto>();

            if (departments is null || departments.Count is 0)
                return new ServiceResult<List<DepartmentDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<List<DepartmentDto>>(departmentDtos,
                        Messages.department_not_found,
                        Messages.department_not_found)
                };

            foreach (var department in departments)
            {
                var faculty = _facultyService.GetById(department.FacultyId).Result;
                if (!faculty.Success)
                {
                    return new ServiceResult<List<DepartmentDto>>
                    {
                        HttpStatusCode = (short)HttpStatusCode.NotFound,
                        Result = new ErrorDataResult<List<DepartmentDto>>(departmentDtos,
                            Messages.faculty_not_found,
                            Messages.faculty_not_found)
                    };
                }

                var departmentDto = new DepartmentDto()
                {
                    Name = department.Name,
                    FacultyName = faculty.Data!.Name,
                    CreatedDate = department.CreatedDate,
                    UpdatedDate = department.UpdatedDate
                };

                departmentDtos.Add(departmentDto);
            }

            return new ServiceResult<List<DepartmentDto>>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<List<DepartmentDto>>(departmentDtos,
                    Messages.success,
                    Messages.success_code)
            };
        }
    }
}