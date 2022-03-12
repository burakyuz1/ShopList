using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification(string productName)
        {
            Query.Where(x => x.Name.ToLower() == productName.ToLower());
        }
    }
}
