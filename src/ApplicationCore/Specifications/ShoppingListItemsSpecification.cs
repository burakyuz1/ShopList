using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ShoppingListItemsSpecification : Specification<ShoppingListItem>
    {
        public ShoppingListItemsSpecification(int? listId)
        {
            if (listId != null)
            {
            Query.Where(x => x.ShoppingListId == listId).Include(x=>x.Product);
            }
        }
    }
}
