using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingAppNew.Web.Models
{
    public class CreateAccountViewModel
    {
        public decimal Balance { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}