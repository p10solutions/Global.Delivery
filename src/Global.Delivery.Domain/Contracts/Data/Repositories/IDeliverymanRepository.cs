using Global.Delivery.Domain.Entities;

namespace Global.Delivery.Domain.Contracts.Data.Repositories
{
    public interface IDeliverymanRepository
    {
        Task AddAsync(DeliverymanEntity Delivery);
        void Update(DeliverymanEntity Delivery);
        Task<DeliverymanEntity?> GetAsync(Guid id);
        Task<IEnumerable<DeliverymanEntity>> GetAsync();
        Task<bool> LicenseNumberExistsAsync(string licenseNumber, Guid deliverymanId);
        Task<bool> DocumentExistsAsync(string document, Guid deliverymanId);
    }
}
