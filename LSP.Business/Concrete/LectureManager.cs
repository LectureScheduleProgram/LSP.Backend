using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Lecture;

namespace LSP.Business.Concrete
{
    public class LectureManager : ILectureService
    {
        private readonly ILectureDal _lectureDal;

        public LectureManager(ILectureDal lectureDal)
        {
            _lectureDal = lectureDal;
        }

        public ServiceResult<bool> Add(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.lecture_name_cant_empty,
                        Messages.lecture_name_cant_empty)
                };
            }

            _lectureDal.Add(new Lecture()
            {
                Name = name
            });
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(Lecture Lecture)
        {
            var getLecture = _lectureDal.Get(x => x.Id == Lecture.Id);
            if (getLecture is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.lecture_not_found,
                        Messages.lecture_not_found)
                };
            }

            _lectureDal.Update(Lecture);
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
            var lecture = _lectureDal.Get(x => x.Id == id);
            if (lecture is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.lecture_not_found,
                        Messages.lecture_not_found)
                };
            }

            _lectureDal.Delete(lecture);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<Lecture> Get(Expression<Func<Lecture, bool>> filter)
        {
            var result = _lectureDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<Lecture>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Lecture>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Lecture>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Lecture>(null,
                    Messages.lecture_not_found,
                    Messages.lecture_not_found)
            };
        }

        public ServiceResult<Lecture> GetById(int id)
        {
            var Lecture = _lectureDal.Get(x => x.Id == id);
            if (Lecture is not null)
            {
                return new ServiceResult<Lecture>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Lecture>(Lecture,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Lecture>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Lecture>(new Lecture(),
                    Messages.lecture_not_found,
                    Messages.lecture_not_found)
            };
        }

        public ServiceResult<List<Lecture>> GetList()
        {
            var lectures = _lectureDal.GetList();
            if (lectures is not null)
            {
                return new ServiceResult<List<Lecture>>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<Lecture>>(lectures.ToList(),
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<List<Lecture>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<Lecture>>(new List<Lecture>(),
                    Messages.lecture_not_found,
                    Messages.lecture_not_found)
            };
        }
    }
}