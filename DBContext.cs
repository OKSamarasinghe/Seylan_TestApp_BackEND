using Microsoft.EntityFrameworkCore;
using Seylan_App_backend_latest.Models;

namespace Seylan_App_backend_latest
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}

