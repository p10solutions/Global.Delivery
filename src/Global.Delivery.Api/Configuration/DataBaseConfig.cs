using Global.Delivery.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Global.Delivery.Api.Configuration
{
    public static class DataBaseConfig
    {
        public static void RunMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<DeliveryManagementContext>();
                context.Database.Migrate();
            }
        }
    }
}
