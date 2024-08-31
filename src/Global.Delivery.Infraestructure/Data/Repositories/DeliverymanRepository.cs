using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Global.Delivery.Infraestructure.Data.Repositories
{
    public class DeliverymanRepository : IDeliverymanRepository
    {
        readonly DeliveryManagementContext _context;

        public DeliverymanRepository(DeliveryManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DeliverymanEntity Delivery)
            => await _context.Deliveryman.AddAsync(Delivery);

        public async Task<DeliverymanEntity?> GetAsync(Guid id)
            => await _context.Deliveryman
                .SingleOrDefaultAsync(x => x.Id == id);

        public void Update(DeliverymanEntity Delivery)
            => _context.Deliveryman.Update(Delivery);

        public async Task<bool> LicenseNumberExistsAsync(string licenseNumber, Guid deliverymanId)
            => await _context.Deliveryman.AnyAsync(x => x.LicenseNumber == licenseNumber && x.Id != deliverymanId);

        public async Task<bool> DocumentExistsAsync(string document, Guid deliverymanId)
            => await _context.Deliveryman.AnyAsync(x => x.Document == document && x.Id != deliverymanId);

        public async Task<IEnumerable<DeliverymanEntity>> GetAsync()
            => await _context.Deliveryman.ToListAsync();
    }
}
