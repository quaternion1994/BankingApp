// Decompiled with JetBrains decompiler
// Type: BankingApp.DataAccess.Entities.BankAccount
// Assembly: BankingApp.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 93060275-A62D-47E4-975C-6521B7D4AC46
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.DataAccess\bin\Debug\BankingApp.DataAccess.dll


using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankingAppNew.DataAccess.Entities
{
    public class BankAccount : IdentityUser
    {
        public long BankAccountId { get; set; }

        [Required(AllowEmptyStrings = false)]

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Decimal AccountBalance { get; set; }

        public virtual IEnumerable<Transfer> Transfers { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BankAccount> manager)
        {
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, "ApplicationCookie");
            return userIdentity;
        }

        public BankAccount()
        {
          this.Transfers = new HashSet<Transfer>();
        }
  }
}
