using ApplicationCore.Interfaces;
using Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Web.Areas.Admin.Interfaces;
using Web.Areas.Admin.Services;

namespace Web
{
    public static class ServiceRegistiration
    {
        public static void WebServiceRegister(this IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            service.AddScoped<IProductViewModelService, ProductViewModelService>();
        }
    }
}
