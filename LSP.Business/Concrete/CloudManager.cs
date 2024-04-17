using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Core.Result;

namespace LSP.Business.Concrete
{
    public class CloudManager : ICloudService
    {
        private readonly IConfiguration _configuration;
        private IAmazonS3 _s3Client;

        public CloudManager(IConfiguration configuration, IAmazonS3 s3Client)
        {
            _configuration = configuration;
            _s3Client = s3Client;
        }

        public async Task<ServiceResult<bool>> UploadImageToS3Async(string base64string, string filename)
        {
            var bucketName = _configuration.GetValue<string>("S3Settings:BucketName");


            byte[] bytes = Convert.FromBase64String(base64string);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var file = new FormFile(ms, 0, ms.Length, null, filename)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/octet-stream"
                };

                var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);

                if (!bucketExists)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.NotFound,
                        Result = new ErrorDataResult<bool>(false, Messages.bucket_not_found, Messages.bucket_not_found)
                    };
                }

                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = filename,
                    InputStream = file.OpenReadStream()
                };
                request.Metadata.Add("Content-Type", file.ContentType);

                var response = await _s3Client.PutObjectAsync(request);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.OK,
                        Result = new SuccessDataResult<bool>(true, Messages.success, Messages.success_code)
                    };
                }
                else
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.InternalServerError,
                        Result = new ErrorDataResult<bool>(false, Messages.s3_upload_error, Messages.s3_upload_error_code)
                    };
                }
            }
        }
    }

}
