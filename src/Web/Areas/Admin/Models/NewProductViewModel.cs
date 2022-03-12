using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
    public class NewProductViewModel
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile Image { get; set; }
        public List<SelectListItem> Categories { get; set; } = new();
    }
}
