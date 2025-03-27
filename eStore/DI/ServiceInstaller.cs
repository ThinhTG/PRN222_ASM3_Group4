
using DataAccess.InterfaceRepo;
using DataAccess.Repositories;
using Services.InterfaceService;
using Services.Service;

namespace eStore.DI
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            //Đăng kí Repo
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

           

            // Đăng ký Service
            services.AddScoped<IMemberService, MemberService>();
        }
    }
}
