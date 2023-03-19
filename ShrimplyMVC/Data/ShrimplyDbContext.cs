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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ShrimpLike> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
