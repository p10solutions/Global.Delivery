using Global.Delivery.Domain.Contracts.Storage;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace Global.Delivery.Infraestructure.Storage
{
    public class DeliverymanStorage : IDeliverymanStorage
    {
        readonly IMinioClient _minioClient;
        readonly string _bucketName;

        public DeliverymanStorage(IConfiguration configuration)
        {
            _minioClient = new MinioClient()
                .WithEndpoint(configuration.GetSection("BlobStorage:Uri").Value)
                .WithCredentials(configuration.GetSection("BlobStorage:AccessKey").Value, configuration.GetSection("BlobStorage:SecretKey").Value)
                .WithSSL(false);

            _bucketName = configuration.GetSection("BlobStorage:BucketName").Value;
        }

        public async Task UploadAsync(MemoryStream licenceImageFile, string licenseImageName)
        {
            bool bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs()
                .WithBucket(_bucketName))
                .ConfigureAwait(false);

            if (!bucketExists)
                await _minioClient
                    .MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName))
                    .ConfigureAwait(false);

            long fileSize = licenceImageFile.Length;
            licenceImageFile.Seek(0, SeekOrigin.Begin);

            await _minioClient.PutObjectAsync
            (
               new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(licenseImageName)
                .WithStreamData(licenceImageFile)
                .WithObjectSize(fileSize)
                .WithContentType("application/octet-stream")
           );
        }

        public async Task<MemoryStream> GetAsync(string licenseImageName)
        {
            var memoryStream = new MemoryStream();

            await _minioClient.GetObjectAsync
            (
                new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(licenseImageName)
                    .WithCallbackStream((stream) =>
                    {
                        stream.CopyTo(memoryStream);
                    })
            );

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}
