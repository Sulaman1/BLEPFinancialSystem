using BLEPFinancialSystem.Models;
using BLEPFinancialSystem.Models.CoreFinancial.Budget;
using BLEPFinancialSystem.Models.CoreFinancial.Contract;
using BLEPFinancialSystem.Models.CoreFinancial.Procurement;
using BLEPFinancialSystem.Models.ProjectManagement.SubProjectManagement;
using BLEPFinancialSystem.Models.WBSpecificModules.Safeguards;
using BLEPFinancialSystem.Models.WBSpecificModules.SpecialAccount;
using BLEPFinancialSystem.Models.WBSpecificModules.Withdrawal;
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
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<ChequeTemplate> ChequeTemplates { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetLineItem> BudgetLineItems { get; set; }
        public DbSet<ProcurementPlan> ProcurementPlans { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractMilestone> ContractMilestones { get; set; }
        public DbSet<ContractPayment> ContractPayments { get; set; }
        public DbSet<SubProject> SubProjects { get; set; }
        public DbSet<SubProjectComponent> SubProjectComponents { get; set; }
        public DbSet<EnvironmentalSafeguard> EnvironmentalSafeguards { get; set; }
        public DbSet<SocialSafeguard> SocialSafeguards { get; set; }
        public DbSet<WithdrawalApplication> WithdrawalApplications { get; set; }
        public DbSet<SupportingDocument> SupportingDocuments { get; set; }
        public DbSet<SpecialAccount> SpecialAccounts { get; set; }
        public DbSet<SpecialAccountTransaction> SpecialAccountTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Existing configurations...
            modelBuilder.Entity<BudgetCategory>()
                .HasOne(b => b.Project)
                .WithMany(p => p.BudgetCategories)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add new configurations
            modelBuilder.Entity<BudgetLineItem>()
                .HasOne(b => b.Budget)
                .WithMany(b => b.LineItems)
                .HasForeignKey(b => b.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bid>()
                .HasOne(b => b.ProcurementPlan)
                .WithMany(p => p.Bids)
                .HasForeignKey(b => b.ProcurementPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContractMilestone>()
                .HasOne(m => m.Contract)
                .WithMany(c => c.Milestones)
                .HasForeignKey(m => m.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContractPayment>()
                .HasOne(p => p.Contract)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubProjectComponent>()
                .HasOne(c => c.SubProject)
                .WithMany(s => s.Components)
                .HasForeignKey(c => c.SubProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupportingDocument>()
                .HasOne(d => d.WithdrawalApplication)
                .WithMany(w => w.Documents)
                .HasForeignKey(d => d.WithdrawalApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SpecialAccountTransaction>()
                .HasOne(t => t.SpecialAccount)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.SpecialAccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}