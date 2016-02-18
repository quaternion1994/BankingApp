using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BankingAppNew.DataAccess
{
    public class BankDbContext : IdentityDbContext<BankAccount>
    {
        public virtual DbSet<Transfer> Transfers { get; set; }

        public BankDbContext()
          : base("name=DefaultConnection")
        {
          Database.SetInitializer<BankDbContext>((IDatabaseInitializer<BankDbContext>) new BankDbContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);
        }

        public static BankDbContext Create()
        {
          return new BankDbContext();
        }
  }
}
public class BankDbContextInitializer : DropCreateDatabaseAlways<BankDbContext>
{
    protected override async void Seed(BankDbContext context)
    {
        BankAccount vlad = new BankAccount()
        {
            Email = "vladislav.petrenko1@gmail.com",
            UserName = "Vlad Petrenko"
        };
        BankUserManager manager = new BankUserManager(new UserStore<BankAccount>(context));
        IdentityResult result1 = await manager.CreateAsync(vlad, "123456");
    }
}