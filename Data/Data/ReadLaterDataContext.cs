using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReadLaterDataContext : IdentityDbContext<ApplicationUser>

    {
        public ReadLaterDataContext(DbContextOptions<ReadLaterDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bookmark>()
                        .HasOne(b => b.Category)
                        .WithMany(c => c.Bookmarks)
                        .HasForeignKey(b => b.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);  //cascade delete
            modelBuilder.Entity<Bookmark>()
                .Property(c => c.CreateDate)
                .HasDefaultValueSql("GETDATE()");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Bookmark> Bookmark { get; set; }
    }
}
