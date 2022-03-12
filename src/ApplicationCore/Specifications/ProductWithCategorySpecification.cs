using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductWithCategorySpecification : Specification<Product>
    {
        public ProductWithCategorySpecification()
        {
            Query.Include(x => x.Category);
        }
    }
}
