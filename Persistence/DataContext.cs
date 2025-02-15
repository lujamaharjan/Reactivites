using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));

        modelBuilder.Entity<ActivityAttendee>()
            .HasOne(u => u.AppUser)
            .WithMany(a => a!.Attendees)
            .HasForeignKey(aa => aa.AppUserId);

        modelBuilder.Entity<ActivityAttendee>()
            .HasOne(a => a.Activity)
            .WithMany(u => u!.Attendees)
            .HasForeignKey(aa => aa.ActivityId);
    }
}