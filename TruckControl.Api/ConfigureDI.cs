using Microsoft.Extensions.DependencyInjection;
using TruckControl.Business.Handlers;
using TruckControl.Business.Handlers.Interfaces;
using TruckControl.Business.Trucks.Repositories;
using TruckControl.Data.Repositories;

namespace TruckControl.Api
{
    public static class ConfigureDI
    {
        public static void AddDIs(this IServiceCollection services)
        {
            AddHandlers(services);
            AddRepositories(services);
        }

        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped<ITrucksHandler, TrucksHandler>();
        }
        
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITrucksRepository, TrucksRepository>();
        }
    }
}
