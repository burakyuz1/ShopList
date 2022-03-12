using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Interfaces
{
    public interface IProductViewModelService
    {
        Task<List<Product>> GetProductsByCategoriesAsync();
        Task<bool> AddProductViewModelAsync(NewProductViewModel model);
        Task DeleteProductAsync(int id);
        Task<UpdateProductViewModel> UpdateProductViewModelAsync(UpdateProductViewModel model);
        Task<UpdateProductViewModel> GetProductByIdAsync(int id);
        Task<NewProductViewModel> GetProductListForAddAsync();
    }
}
