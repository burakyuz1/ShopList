using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ShoppingListNonMemberSpecification : Specification<ShoppingListMember>
    {
        public ShoppingListNonMemberSpecification(int listId)
        {
            Query.Where(x => x.ShoppingListId != listId);
        }
    }
}
