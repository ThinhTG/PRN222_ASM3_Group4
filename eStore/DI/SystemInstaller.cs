
using CloudinaryDotNet;
using DataAccess.DBContext;
using DataAccess.Mapping;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using Services.DTOs;
using BusinessObject.Models;

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

            services.AddSingleton<Cloudinary>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var cloudName = configuration["Cloudinary:CloudName"];
                var apiKey = configuration["Cloudinary:ApiKey"];
                var apiSecret = configuration["Cloudinary:ApiSecret"];

                if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                {
                    throw new InvalidOperationException("Cloudinary configuration is missing in appsettings.json.");
                }

                return new Cloudinary(new Account(cloudName, apiKey, apiSecret));
            });

            // Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            // Tạo tài khoản Admin mặc định
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<eStoreContext>();
                CreateDefaultAdmin(context);
            }
        }
        private void CreateDefaultAdmin(eStoreContext context)
        {
            if (!context.Members.Any(m => m.Role == "Admin"))
            {
                string adminEmail = "admin@estore.com";
                string adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");

                var adminUser = new Member
                {
                    Email = adminEmail,
                    Password = adminPassword,
                    CompanyName = "eStore Admin",
                    City = "Hanoi",
                    Country = "Vietnam",
                    Role = "Admin"
                };

                context.Members.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
}
