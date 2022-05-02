using Microsoft.EntityFrameworkCore;
using WebApiDataLayer.Models;

namespace WebApiDataLayer
{
    public class WebApiDataContext : DbContext
    {
        public virtual DbSet<Kitten> Kittens { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }

        public WebApiDataContext(DbContextOptions<WebApiDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
