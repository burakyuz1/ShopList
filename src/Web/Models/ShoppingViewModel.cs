using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ShoppingViewModel
    {
        public int? CategoryId { get; set; }
        public int? ShoppingListId { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<ShoppingListItem> Items { get; set; } 

    }
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public string Description { get; set; }
    }
}
