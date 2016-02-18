using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Repositories;
using System;

namespace BankingAppNew.DataAccess.Repositories.Implementation
{
  public class AccountUnitOfWork : IDisposable
  {
    private bool disposed = false;
    private BankDbContext _context;
    private IBankAccountRepository _bankAccountRepository;
    private ITransferRepository _transferRepository;

    public IBankAccountRepository BankAccountRepository
    {
      get
      {
        if (this._bankAccountRepository == null)
          this._bankAccountRepository = new BankingAppNew.DataAccess.Repositories.Implementation.BankAccountRepository(this._context);
        return this._bankAccountRepository;
      }
    }

    public ITransferRepository TransferRepository
    {
      get
      {
        if (this._transferRepository == null)
          this._transferRepository = (ITransferRepository) new BankingAppNew.DataAccess.Repositories.Implementation.TransferRepository(this._context);
        return this._transferRepository;
      }
    }

    public AccountUnitOfWork(BankDbContext context)
    {
      this._context = context;
    }

    private void Save()
    {
      this._context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed && disposing)
        {
            this.Save();
            _context.Dispose();
            this.disposed = true;
        }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
