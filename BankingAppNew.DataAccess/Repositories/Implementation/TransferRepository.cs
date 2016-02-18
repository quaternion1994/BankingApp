// Decompiled with JetBrains decompiler
// Type: BankingApp.DataAccess.Repositories.Implementation.TransferRepository
// Assembly: BankingApp.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 93060275-A62D-47E4-975C-6521B7D4AC46
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.DataAccess\bin\Debug\BankingApp.DataAccess.dll

using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Entities;
using BankingAppNew.DataAccess.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BankingAppNew.DataAccess.Repositories.Implementation
{
  public class TransferRepository : Repository<Transfer, long>, ITransferRepository
  {
    public TransferRepository(BankDbContext context)
      : base(context)
    {
    }

    public override Transfer GetByID(long id)
    {
        return this._context.Transfers.Single(x => x.TransferId == id);
    }

    public override IQueryable<Transfer> All()
    {
        return this._context.Transfers.Include(x => x.BankAccount);
    }

    public override IQueryable<Transfer> All(Expression<Func<Transfer, bool>> query)
    {
        return this._context.Transfers.Include(x=>x.BankAccount).Where(query);
    }
  }
}
