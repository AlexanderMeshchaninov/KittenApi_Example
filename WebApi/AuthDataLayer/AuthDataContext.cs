using AuthDataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthDataLayer
{
    public class AuthDataContext : DbContext
    {
        public DbSet<User> Clients { get; set; }

        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
