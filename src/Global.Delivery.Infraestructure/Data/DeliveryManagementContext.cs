using Global.Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Global.Delivery.Infraestructure.Data
{
    public class DeliveryManagementContext(DbContextOptions<DeliveryManagementContext> options) : DbContext(options)
    {
        public DbSet<DeliverymanEntity> Deliveryman { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
