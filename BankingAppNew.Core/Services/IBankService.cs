// Decompiled with JetBrains decompiler
// Type: BankingApp.Core.IBankService
// Assembly: BankingApp.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D889DEC-BB19-4023-A15B-5B20AAAAE8B7
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.Core\bin\Debug\BankingApp.Core.dll

using BankingAppNew.Common;
using BankingAppNew.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingAppNew.Core.Services
{
  public interface IBankService
  {
    BankRequestResult CreateAccount(BankAccount account);

    BankRequestResult DeleteAccount(string id);

    BankRequestResult Deposit(string userid, Decimal amount);

    BankRequestResult<Decimal> GetBalance(string userid);

    BankRequestResult<IEnumerable<Transfer>> GetUserStatement(string accountid);

    BankRequestResult Transfer(string sourceUserId, string destinationUserId, decimal amount);

    BankRequestResult Withdraw(string userid, decimal amount);

    BankRequestResult<IEnumerable<BankAccount>> AccountList();
  }
}
