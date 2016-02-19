// Decompiled with JetBrains decompiler
// Type: BankingApp.DataAccess.Repositories.Implementation.Repository`1
// Assembly: BankingApp.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 93060275-A62D-47E4-975C-6521B7D4AC46
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.DataAccess\bin\Debug\BankingApp.DataAccess.dll

using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BankingAppNew.DataAccess.Repositories.Implementation
{
  public abstract class Repository<T, TKey> : IRepository<T, TKey> where T : class
  {
    protected readonly BankDbContext _context;

    public Repository(BankDbContext context)
    {
      this._context = context;
    }

    public abstract T GetByID(TKey id);

    public virtual T Find(Expression<Func<T, bool>> query)
    {
      return Queryable.SingleOrDefault<T>(this._context.Set<T>(), query);
    }

    public virtual IQueryable<T> All()
    {
      return Queryable.AsQueryable<T>(this._context.Set<T>());
    }

    public virtual IQueryable<T> All(Expression<Func<T, bool>> query)
    {
      return _context.Set<T>().Where( query);
    }

    public virtual void Add(T entity)
    {
        this._context.Set<T>().Add(entity);
    }

    public virtual void Delete(T entity)
    {
      this._context.Set<T>().Remove(entity);
    }

    public virtual void Save()
    {
      this._context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
      this._context.Entry<T>(entity).State = EntityState.Modified;
    }
  }
}
