namespace Global.Delivery.Domain.Contracts.Storage
{
    public interface IDeliverymanStorage
    {
        Task UploadAsync(MemoryStream licenceImageFile, string licenseImageName);
        Task<MemoryStream> GetAsync(string licenseImageName);
    }
}
