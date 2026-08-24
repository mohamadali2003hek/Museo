using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Museo.Models;
using System.Security.Principal;

namespace Museo.Museo_Database_Context
{
    public class MuseoDbContext : IdentityDbContext<Users>
    {
        public DbSet<Items> Items { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Saves> Saves { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<User_Interests> UserInterests { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Follows> Follows { get; set; }
        public DbSet<Comments> Comments { get; set; }


        public MuseoDbContext(DbContextOptions<MuseoDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Follows Composite Key
            modelBuilder.Entity<Follows>()
                .HasKey(f => new { f.follower_id, f.following_id });

            modelBuilder.Entity<Items>()
            .HasMany(i => i.Tags)
            .WithMany(t => t.Items);

            // User Interests Composite Key
            modelBuilder.Entity<User_Interests>()
                .HasKey(ui => new { ui.UserId, ui.TagId });

            // Comments Self Relation
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Items>()
            .HasOne(i => i.Profile)
            .WithMany()
            .HasForeignKey(i => i.ProfileId)
            .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Follows>()
            .HasOne(f => f.follower)
            .WithMany()
            .HasForeignKey(f => f.follower_id)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Follows>()
                .HasOne(f => f.following)
                .WithMany()
                .HasForeignKey(f => f.following_id)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
