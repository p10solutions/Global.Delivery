using Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman;
using Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman;
using Global.Delivery.Domain.Contracts.Data;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Events;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Contracts.Storage;
using Global.Delivery.Infraestructure.Data;
using Global.Delivery.Infraestructure.Data.Repositories;
using Global.Delivery.Infraestructure.Events.Deliveryman;
using Global.Delivery.Infraestructure.Storage;
using Global.Delivery.Infraestructure.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Global.Delivery.Infraestructure.IoC
{
    public static class DeliveryManagementProvider
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            var connectionString = configuration.GetConnectionString("DeliveryManagement");


            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastValidator<,>));
            services.AddScoped<INotificationsHandler, NotificationHandler>();
            services.AddDbContextPool<DeliveryManagementContext>(opt => opt.UseNpgsql(connectionString));
            services.AddTransient<IDeliverymanRepository, DeliverymanRepository>();
            services.AddTransient<IDeliverymanProducer, DeliverymanProducer>();
            services.AddTransient<IDeliverymanStorage, DeliverymanStorage>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(CreateDeliverymanMapper));
            services.AddAutoMapper(typeof(UpdateDeliverymanMapper));

            return services;
        }
    }
}
