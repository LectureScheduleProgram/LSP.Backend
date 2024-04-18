using LSP.Core.Result;

namespace LSP.Business.Abstract
{
    public interface ICloudService
    {
        Task<ServiceResult<bool>> UploadImageToS3Async(string base64string, string filename);
    }
}