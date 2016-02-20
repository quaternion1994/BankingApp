using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingAppNew.Web.Models
{
    public class TransactionViewModel
    {
        public long TransferId { get; set; }

        public string Username { get; set; }

        public Decimal Amount { get; set; }

        public virtual string BankAccountId { get; set; }

    }
}