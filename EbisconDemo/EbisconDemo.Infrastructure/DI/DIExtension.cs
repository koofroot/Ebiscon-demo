using EbisconDemo.Data;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EbisconDemo.Services.Mapping;
using EbisconDemo.Data.Models;

namespace EbisconDemo.Infrastructure.DI
{
    public static class DIExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDataRetrieveService, DataRetrieveService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISynchronizationService, SynchronizationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<INotificationService, NotificationService>();
    }

        public static void AddData(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void ConfigureSetting<T>(this IServiceCollection services, IConfigurationRoot configuration) where T : class, new()
        {
            var settingClass = new T();

            configuration.GetSection(typeof(T).Name).Bind(settingClass);

            services.AddSingleton<T>(settingClass);
        }

        public static void AddContext(this IServiceCollection services, string connectionString)
        {            
            services.AddDbContextFactory<Context>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServicesProfile));
        }
    }
}
