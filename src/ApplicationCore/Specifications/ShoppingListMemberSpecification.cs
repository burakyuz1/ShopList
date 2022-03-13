using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ShoppingListMemberSpecification : Specification<ShoppingListMember>
    {
        public ShoppingListMemberSpecification(string memberId)
        {
            Query.Where(x => x.UserId == memberId);
        }
    }
}
