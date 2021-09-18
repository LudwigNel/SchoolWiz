using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolWiz.Entity;
using SchoolWiz.Persistence.Extensions;

namespace SchoolWiz.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<GuardianType> GuardianTypes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGuardian> StudentGuardians { get; set; }
        public DbSet<Vat> Vat { get; set; }
        public DbSet<GuardianAddress> GuardianAddresses { get; set; }
        public DbSet<StudentRegistration> StudentRegistrations { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<AccountRate> AccountRates { get; set; }
        public DbSet<InvoiceRun> InvoiceRuns { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SetSchemas();
            builder.CreateIndexes();
            builder.SeedData();

            base.OnModelCreating(builder);
        }
    }
}
