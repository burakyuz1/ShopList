using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class IdentityShopListDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityShopListDbContext(DbContextOptions<IdentityShopListDbContext> options) : base(options)
        {
        }
    }
}
