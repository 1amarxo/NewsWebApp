using Microsoft.EntityFrameworkCore;
using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext( DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.News)
                .WithMany(n => n.NewsTags)
                .HasForeignKey(nt=>nt.NewsId);

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NewsTags)
                .HasForeignKey(nt => nt.TagId);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<News> News { get; set; }
    }
}
