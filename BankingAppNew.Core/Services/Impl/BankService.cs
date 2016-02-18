using BankingAppNew.Common;
using BankingAppNew.DataAccess.Entities;
using BankingAppNew.DataAccess.Repositories;
using BankingAppNew.DataAccess.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace BankingAppNew.Core.Services.Impl
{
  public class BankService : IBankService
  {
    private IUnitOfWorkFactory _factory;
    private const int AttemptsCount = 10;
    public BankService(IUnitOfWorkFactory factory)
    {
      this._factory = factory;
    }

    public BankRequestResult<Decimal> GetBalance(string userid)
    {
      using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
        return new BankRequestResult<Decimal>(BankRequestStatus.Done, accountUnitOfWork.BankAccountRepository.GetByID(userid).AccountBalance);
    }

      public BankRequestResult Deposit(string userid, Decimal amount)
      {
          bool done = false;
          int iterations = 1;
          while (iterations<AttemptsCount && !done)
          {
              iterations++;
              try
              {
                  using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
                  {
                      BankAccount account = accountUnitOfWork.BankAccountRepository.GetByID(userid);
                      account.AccountBalance += amount;
                      accountUnitOfWork.TransferRepository.Add(new Transfer()
                      {
                          Amount = amount,
                          BankAccountId = account.Id,
                          Username = account.UserName
                      });
                      accountUnitOfWork.BankAccountRepository.Update(account);
                  }
                  done = true;
              }
              catch (DbUpdateConcurrencyException ex)
              {
                  done = false;
                  Thread.Sleep(1);
              }
          }
          if(done)
                return new BankRequestResult();
          else
                return new BankRequestResult(BankRequestStatus.Error, null, "Request timeout");
    }

    public BankRequestResult Withdraw(string userid, decimal amount)
    {
        bool done = false;
        int iterations = 1;
        while (iterations < AttemptsCount && !done)
        {
            iterations++;
            try
            {
                using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
                {
                    BankAccount account = accountUnitOfWork.BankAccountRepository.GetByID(userid);

                    if (account.AccountBalance < amount)
                    {
                        done = false;
                        return new BankRequestResult(BankRequestStatus.Error, null, "Not enough money");
                    }

                    account.AccountBalance -= amount;

                    accountUnitOfWork.TransferRepository.Add(new Transfer()
                    {
                        Amount = -amount,
                        BankAccountId = account.Id,
                        Username = account.UserName
                    });

                    accountUnitOfWork.BankAccountRepository.Update(account);
                    done = true;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                done = false;
                Thread.Sleep(1);
            }
        }
        if (done)
                return new BankRequestResult();
        else
                return new BankRequestResult(BankRequestStatus.Error, null, "Request timeout");
    }

      public BankRequestResult Transfer(string sourceUserId, string destinationUserId, decimal amount)
      {
        bool done = false;
        int iterations = 1;

        while (iterations < AttemptsCount && !done)
        {
            using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
            {
                try
                {
                    BankAccount destAccount = accountUnitOfWork.BankAccountRepository.GetByID(destinationUserId);
                    BankAccount srcAccount = accountUnitOfWork.BankAccountRepository.GetByID(sourceUserId);

                    if (srcAccount.AccountBalance < amount)
                    {
                        done = false;
                        return new BankRequestResult(BankRequestStatus.Error, null, "Not enouth money");
                    }
                    srcAccount.AccountBalance -= amount;
                    destAccount.AccountBalance += amount;

                    accountUnitOfWork.TransferRepository.Add(new Transfer()
                    {
                        Amount = amount,
                        BankAccountId = destAccount.Id,
                        Username = destAccount.UserName
                    });

                    accountUnitOfWork.TransferRepository.Add(new Transfer()
                    {
                        Amount = -amount,
                        BankAccountId = srcAccount.Id,
                        Username = srcAccount.UserName
                    });

                    accountUnitOfWork.BankAccountRepository.Update(srcAccount);
                    accountUnitOfWork.BankAccountRepository.Update(destAccount);

                    done = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    done = false;
                    Thread.Sleep(1);
                }
            }
        }

        if (done)
            return new BankRequestResult();
        else
            return new BankRequestResult(BankRequestStatus.Error, null, "Request timeout");
    }

    public BankRequestResult CreateAccount(BankAccount account)
    {
        using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
        {
            accountUnitOfWork.BankAccountRepository.Add(account);
        }
        return new BankRequestResult();
    }

    public BankRequestResult DeleteAccount(string id)
    {
        using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
        {
            accountUnitOfWork.BankAccountRepository.Delete(accountUnitOfWork.BankAccountRepository.GetByID(id));
        }
        return new BankRequestResult();
    }

    public BankRequestResult<IEnumerable<Transfer>> GetUserStatement(string accountid)
    {
        using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
        {
            return new BankRequestResult<IEnumerable<Transfer>>(BankRequestStatus.Done,
                accountUnitOfWork.TransferRepository.All(x => x.BankAccountId == accountid));
        }
    }
    
    public BankRequestResult<IQueryable<BankAccount>> AccountList()
    {
        using (AccountUnitOfWork accountUnitOfWork = this._factory.CreateAccountUnitOfWork())
        {
            return new BankRequestResult<IQueryable<BankAccount>>(BankRequestStatus.Done, accountUnitOfWork.BankAccountRepository.All());
        }
    }
  }
}
