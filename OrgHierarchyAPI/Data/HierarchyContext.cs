using Microsoft.EntityFrameworkCore;
using System;

namespace OrgHierarchyAPI.Models
{
    public class HierarchyContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }

        public HierarchyContext(DbContextOptions<HierarchyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the self-referencing relationship for Position
            modelBuilder.Entity<Position>()
                .HasOne(p => p.Parent) // Each Position can have one Parent
                .WithMany(p => p.Children) // Each Parent can have many Children
                .HasForeignKey(p => p.ParentId) // Use ParentId as the foreign key
                .IsRequired(false) // ParentId can be null (for positions like CEO)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }

    } }
