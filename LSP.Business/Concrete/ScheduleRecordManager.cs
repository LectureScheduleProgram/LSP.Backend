﻿using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;

namespace LSP.Business.Concrete
{
    public class ScheduleRecordManager : IScheduleRecordService
    {
        private readonly IScheduleRecordDal _scheduleRecordDal;

        public ScheduleRecordManager(IScheduleRecordDal scheduleRecordDal)
        {
            _scheduleRecordDal = scheduleRecordDal;
        }
        public ServiceResult<bool> Add(ScheduleRecord ScheduleRecord)
        {
            _scheduleRecordDal.Add(ScheduleRecord);
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
    }
}