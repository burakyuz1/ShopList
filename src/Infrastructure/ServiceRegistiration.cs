using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void InfrastructureServiceRegister(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<ShopListDbContext>(x => x.UseSqlServer(config.GetConnectionString("ShopListDb")));
            service.AddDbContext<IdentityShopListDbContext>(options => options.UseSqlServer(config.GetConnectionString("ShopListDbIdentity")));
            service.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<IdentityShopListDbContext>();
        }
    }
}
