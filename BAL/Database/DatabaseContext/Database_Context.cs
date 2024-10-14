using BAL.Database.Company;
using BAL.Database.DatabaseIdentity;
using BAL.Database.DatabaseModels;
using BAL.Database.RequestCompany;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.DatabaseContext
{
    public class Database_Context : IdentityDbContext<User, Role, string>
    {
        public Database_Context(DbContextOptions<Database_Context> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<NewCompanyRequest> NewCompanyReqs { get; set; }
        public DbSet<CompanyData> CompanyDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(k => k.userInfo)
                .WithOne(ko => ko.user)
                .HasForeignKey<UserInfo>(ko => ko.userId);
        }
    }
}

