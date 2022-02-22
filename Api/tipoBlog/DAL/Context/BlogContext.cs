using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .HasKey(o => new { o.PublisherId, o.SubscriberId });

            modelBuilder.Entity<Subscription>()
                              .HasOne(sub => sub.Publisher)
                              .WithMany(user => user.Subscribers) // <--
                              .HasForeignKey(sub => sub.PublisherId)
                              .OnDelete(DeleteBehavior.Restrict); // see the note at the end

            modelBuilder.Entity<Subscription>()
                .HasOne(sub => sub.Subscriber)
                .WithMany(user => user.Publishers)
                .HasForeignKey(sub => sub.SubscriberId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
