using Apparal_Management.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Apparal_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.ApplicationUser>   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
