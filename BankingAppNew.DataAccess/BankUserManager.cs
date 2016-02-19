using BankingAppNew.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity;

namespace BankingAppNew.DataAccess
{
    public class BankUserManager : UserManager<BankAccount>
    {
        public BankUserManager(IUserStore<BankAccount> store): base(store)
        {
        }

        public static BankUserManager Create(IdentityFactoryOptions<BankUserManager> options, IOwinContext context)
        {
            return new BankUserManager(new UserStore<BankAccount>(OwinContextExtensions.Get<BankDbContext>(context)));
        }
  }
}
