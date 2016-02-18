// Decompiled with JetBrains decompiler
// Type: BankingApp.DataAccess.Repositories.IUnitOfWorkFactory
// Assembly: BankingApp.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 93060275-A62D-47E4-975C-6521B7D4AC46
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.DataAccess\bin\Debug\BankingApp.DataAccess.dll

using BankingAppNew.DataAccess.Repositories.Implementation;

namespace BankingAppNew.DataAccess.Repositories
{
  public interface IUnitOfWorkFactory
  {
    AccountUnitOfWork CreateAccountUnitOfWork();
  }
}
