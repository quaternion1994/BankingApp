// Decompiled with JetBrains decompiler
// Type: BankingApp.DataAccess.Entities.Transfer
// Assembly: BankingApp.DataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 93060275-A62D-47E4-975C-6521B7D4AC46
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.DataAccess\bin\Debug\BankingApp.DataAccess.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAppNew.DataAccess.Entities
{
  public class Transfer
  {
    public long TransferId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; }

    public Decimal Amount { get; set; }
    

    public virtual string BankAccountId { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    [ForeignKey("BankAccountId")]
    public virtual BankAccount BankAccount { get; set; }
  }
}
