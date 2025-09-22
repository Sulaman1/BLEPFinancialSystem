using BLEPFinancialSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BLEPFinancialSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<BudgetCategory>()
                .HasOne(b => b.Project)
                .WithMany(p => p.BudgetCategories)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Disbursement>()
                .HasOne(d => d.Project)
                .WithMany(p => p.Disbursements)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Beneficiary)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BeneficiaryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Disbursement)
                .WithMany(d => d.Payments)
                .HasForeignKey(p => p.DisbursementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}