using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingAppNew.Web.Models
{
    public class TransferViewModel
    {
        public string DestanationId { get; set; }

        public decimal Amount { get; set; }
    }
}