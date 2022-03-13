using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Web.Areas.Admin.Interfaces;
using Web.Areas.Admin.Services;
using Web.Interfaces;
using Web.Services;

namespace Web
{
    public static class ServiceRegistiration
    {
        public static void WebServiceRegister(this IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            service.AddScoped<IListService, ListService>();
            service.AddScoped<IProductViewModelService, ProductViewModelService>();
            service.AddScoped<IShoppingViewModelService, ShoppingViewModelService>();
            service.AddScoped<IShoppingListViewModelService, ShoppingListViewModelService>();
        }
    }
}
