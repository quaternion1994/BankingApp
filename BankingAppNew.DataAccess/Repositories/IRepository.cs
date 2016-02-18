// Decompiled with JetBrains decompiler
// Type: BankingApp.DataAccess.Repositories.IRepository`1
// Assembly: BankingApp.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 93060275-A62D-47E4-975C-6521B7D4AC46
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.DataAccess\bin\Debug\BankingApp.DataAccess.dll

using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankingAppNew.DataAccess.Repositories
{
  public interface IRepository<T, TKey> where T : class
  {
        T GetByID(TKey id);

        T Find(Expression<Func<T, bool>> query);

        IQueryable<T> All();

        IQueryable<T> All(Expression<Func<T, bool>> query);

        void Add(T entity);

        void Delete(T entity);

        void Save();

        void Update(T entity);
  }
}
