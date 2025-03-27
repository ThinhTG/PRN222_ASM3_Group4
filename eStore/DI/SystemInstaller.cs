
using DataAccess.DBContext;
using DataAccess.Mapping;
using Microsoft.EntityFrameworkCore;

namespace eStore.DI
{
    public class SystemInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<eStoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbContext"));
            });


            // Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

        }
    }
}
