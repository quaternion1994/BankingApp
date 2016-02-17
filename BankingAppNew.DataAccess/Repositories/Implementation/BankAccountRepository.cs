// Decompiled with JetBrains decompiler
// Type: BankingApp.DataAccess.Repositories.Implementation.BankAccountRepository
// Assembly: BankingApp.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 93060275-A62D-47E4-975C-6521B7D4AC46
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.DataAccess\bin\Debug\BankingApp.DataAccess.dll

using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Entities;
using BankingAppNew.DataAccess.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankingAppNew.DataAccess.Repositories.Implementation
{
  public class BankAccountRepository : Repository<BankAccount, string>, IBankAccountRepository
  {
    public BankAccountRepository(BankDbContext context)
      : base(context)
    {
    }

    public override BankAccount GetByID(string id)
    {
      return _context.Users.Single<BankAccount>(x => x.Id == id);
    }
  }
}
