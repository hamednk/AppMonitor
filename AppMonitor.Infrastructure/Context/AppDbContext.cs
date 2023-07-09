using AppMonitor.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppMonitor.Infrastructure.Context
{
    public class TargetAppDbContext : IdentityDbContext<User>
    {
        public TargetAppDbContext(DbContextOptions<TargetAppDbContext> options) : base(options)
        {
        }

        public DbSet<TargetApp> Apps { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TargetApp>()
            .HasOne(a => a.User)
            .WithMany(u => u.TargetApps)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
