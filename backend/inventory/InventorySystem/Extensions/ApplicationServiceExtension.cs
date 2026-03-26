using InventorySystem.Application.Interfaces;
using InventorySystem.Application.Mapping;
using InventorySystem.Application.Services;
using InventorySystem.Domain.Interfaces;
using InventorySystem.Infrastracture.Repositories;
using InventorySystem.Infrastracture.Services;
namespace InventorySystem.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            Services.AddScoped<IProductService,ProductService>();
            Services.AddScoped<ISaleService,SaleService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
          
            Services.AddScoped<IAuthService, AuthService>();
            //Services.AddScoped<IGenericRepository<T>, GenericRepository<T>>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
}
