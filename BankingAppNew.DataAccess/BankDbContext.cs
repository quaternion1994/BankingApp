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
          Database.SetInitializer<BankDbContext>( new BankDbContextInitializer() );
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
    protected override void Seed(BankDbContext context)
    {
        BankAccount vlad = new BankAccount()
        {
            Email = "vladislav.petrenko1@gmail.com",
            EmailConfirmed = true,
            AccountBalance = 0,
            UserName = "Vlad"
        };

        BankUserManager manager = new BankUserManager(new UserStore<BankAccount>(context));
        IdentityResult result1 = manager.Create(vlad, "123456");

        base.Seed(context);
        if (result1 != null)
        {
            
        }
    }
}