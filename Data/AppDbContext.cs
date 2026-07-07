using Microsoft.EntityFrameworkCore;
using QuickPortals.Models;

namespace QuickPortals.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<SetupCheck> SetupChecks { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
