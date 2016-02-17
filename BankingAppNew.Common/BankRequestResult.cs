using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppNew.Common
{
    public enum BankRequestStatus { Error, Done }
    public class BankRequestResult<T>
    {
        public BankRequestResult
            (BankRequestStatus status = BankRequestStatus.Done, T value = default(T), string message = "")
        {
            this.Message = message;
            this.Status = status;
            this.Value = value;
        }
        public BankRequestStatus Status { get; set; }
        public string Message { get; set; }
        public T Value { get;set; }
    }
    public class BankRequestResult : BankRequestResult<object>
    {
        public BankRequestResult(BankRequestStatus status = BankRequestStatus.Done, object value = null, string message = "")
            : base(status, value, message)
        {
        }
    }
}
