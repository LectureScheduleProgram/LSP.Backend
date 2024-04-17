using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSP.Core.Result;
using LSP.Entity.DTO.Kyc;

namespace LSP.Business.Abstract
{
    public interface ICloudService
    {
        Task<ServiceResult<bool>> UploadImageToS3Async(string base64string, string filename);

    }
}
