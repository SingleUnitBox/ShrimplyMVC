using Microsoft.EntityFrameworkCore;
using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Data
{
    public class ShrimplyDbContext : DbContext
    {
        public ShrimplyDbContext(DbContextOptions<ShrimplyDbContext> options) : base(options)
        {
        }
        public DbSet<Shrimp> Shrimps { get; set; }
    }
}
