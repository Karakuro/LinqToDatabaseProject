using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinqToDatabaseProject.Data
{
    public class GameDbContext : IdentityDbContext<ApplicationUser>
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Rarity> Rarities { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
    }
}
