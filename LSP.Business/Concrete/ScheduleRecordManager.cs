using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.ScheduleRecord;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Business.Concrete
{
    public class ScheduleRecordManager : IScheduleRecordService
    {
        private readonly IScheduleRecordDal _scheduleRecordDal;

        public ScheduleRecordManager(IScheduleRecordDal scheduleRecordDal)
        {
            _scheduleRecordDal = scheduleRecordDal;
        }

        public ServiceResult<bool> Add(AddScheduleRecordDto request)
        {
            if (request.StartHour == request.EndHour)
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(
                        false,
                        Messages.same_start_end_hour,
                        Messages.same_start_end_hour)
                };

            if (request.StartHour > request.EndHour)
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(
                        false,
                        Messages.start_hour_must_smaller,
                        Messages.start_hour_must_smaller)
                };

            var scheduleRecord = _scheduleRecordDal.Get(record =>
                record.ClassroomId == request.ClassroomId &&
                record.Day == request.Day &&
                (
                    (record.StartHour == request.StartHour && record.EndHour == request.EndHour) ||
                    (record.StartHour == request.StartHour && record.EndHour < request.EndHour) ||
                    (record.StartHour == request.StartHour && record.EndHour > request.EndHour) ||
                    (record.StartHour < request.StartHour && record.EndHour == request.EndHour) ||
                    (record.StartHour < request.StartHour && record.EndHour > request.EndHour) ||
                    (record.StartHour < request.StartHour && record.EndHour < request.EndHour) ||
                    (record.StartHour > request.StartHour && record.EndHour == request.EndHour) ||
                    (record.StartHour > request.StartHour && record.EndHour > request.EndHour) ||
                    (record.StartHour > request.StartHour && record.EndHour < request.EndHour)
                )
            );

            if (scheduleRecord is not null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.scheduleRecord_already_exists,
                        Messages.scheduleRecord_already_exists)
                };
            }

            _scheduleRecordDal.Add(new ScheduleRecord
            {
                ClassroomId = request.ClassroomId,
                LectureId = request.LectureId,
                Day = request.Day,
                StartHour = request.StartHour,
                EndHour = request.EndHour
            });

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(ScheduleRecord ScheduleRecord)
        {
            var getScheduleRecord = _scheduleRecordDal.Get(x => x.Id == ScheduleRecord.Id);
            if (getScheduleRecord is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.scheduleRecord_not_found,
                        Messages.scheduleRecord_not_found)
                };
            }

            _scheduleRecordDal.Update(ScheduleRecord);
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
            var ScheduleRecord = _scheduleRecordDal.Get(x => x.Id == id);
            if (ScheduleRecord is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.scheduleRecord_not_found,
                        Messages.scheduleRecord_not_found)
                };
            }

            _scheduleRecordDal.Delete(ScheduleRecord);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<ScheduleRecord> Get(Expression<Func<ScheduleRecord, bool>> filter)
        {
            var result = _scheduleRecordDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<ScheduleRecord>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<ScheduleRecord>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<ScheduleRecord>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<ScheduleRecord>(null,
                    Messages.scheduleRecord_not_found,
                    Messages.scheduleRecord_not_found)
            };
        }

        public ServiceResult<ScheduleRecord> GetById(int id)
        {
            var ScheduleRecord = _scheduleRecordDal.Get(x => x.Id == id);
            if (ScheduleRecord is not null)
            {
                return new ServiceResult<ScheduleRecord>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<ScheduleRecord>(ScheduleRecord,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<ScheduleRecord>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<ScheduleRecord>(new ScheduleRecord(),
                    Messages.scheduleRecord_not_found,
                    Messages.scheduleRecord_not_found)
            };
        }

        public ServiceResult<List<ScheduleRecord>> GetList()
        {
            var ScheduleRecords = _scheduleRecordDal.GetList();
            if (ScheduleRecords is not null)
            {
                return new ServiceResult<List<ScheduleRecord>>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<ScheduleRecord>>(ScheduleRecords.ToList(),
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<List<ScheduleRecord>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<ScheduleRecord>>(new List<ScheduleRecord>(),
                    Messages.scheduleRecord_not_found,
                    Messages.scheduleRecord_not_found)
            };
        }

        public ServiceResult<List<ScheduleRecord>> GetListByClassroomIds(List<short> classroomIds)
        {
            var ScheduleRecords = _scheduleRecordDal.GetList(sr => classroomIds.Contains(sr.ClassroomId));
            if (ScheduleRecords is not null)
            {
                return new ServiceResult<List<ScheduleRecord>>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<ScheduleRecord>>(ScheduleRecords.ToList(),
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<List<ScheduleRecord>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<ScheduleRecord>>(new List<ScheduleRecord>(),
                    Messages.scheduleRecord_not_found,
                    Messages.scheduleRecord_not_found)
            };
        }

        public bool TimeControl(List<ScheduleRecord> list, DaysEnum day, byte startHour, byte endHour)
        {
            return list.Exists(record =>
                record.Day == day &&
                (
                    (record.StartHour == startHour && record.EndHour == endHour) ||
                    (record.StartHour == startHour && record.EndHour < endHour) ||
                    (record.StartHour == startHour && record.EndHour > endHour) ||
                    (record.StartHour < startHour && record.EndHour == endHour) ||
                    (record.StartHour < startHour && record.EndHour > endHour) ||
                    (record.StartHour < startHour && record.EndHour < endHour) ||
                    (record.StartHour > startHour && record.EndHour == endHour) ||
                    (record.StartHour > startHour && record.EndHour > endHour) ||
                    (record.StartHour > startHour && record.EndHour < endHour)
                )
            );
        }
    }
}