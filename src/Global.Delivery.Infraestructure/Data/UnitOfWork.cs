using Global.Delivery.Domain.Contracts.Data;

namespace Global.Delivery.Infraestructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        readonly DeliveryManagementContext _context;

        public UnitOfWork(DeliveryManagementContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
