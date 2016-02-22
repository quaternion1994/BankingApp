using BankingAppNew.Common;
using BankingAppNew.Core.Services;
using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Entities;
using BankingAppNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BankingAppNew.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BankingApp.Web.Controllers
{
    [RoutePrefix("api/BankAccount")]
    [Authorize]
    public class BankAccountController : ApiController
    {
        IBankService _service;
        public BankUserManager UserManager { get; private set; }

        public BankAccountController(IBankService service)
        {
            this._service = service;
            this.UserManager = Startup.UserManagerFactory();
        }
        // GET api/BankAccount/AccountList
        // Получить список
        [Route("AccountList")]
        [AllowAnonymous]
        public IHttpActionResult GetAccountList()
        {
            var result = _service.AccountList();

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);

            return Ok<IEnumerable<AccountListViewModel>>(_service.AccountList().
                                    Value.Select(item => new AccountListViewModel()
                                    {
                                        Id = item.Id,
                                        UserName = item.UserName,
                                        AccountBalance = item.AccountBalance
                                    }));
        }

        [Route("UserInfo")]
        public IHttpActionResult GetUserInfo()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
                return BadRequest();

            return Ok(new AccountInfoViewModel()
            {
                Username = user.UserName,
                Id = user.Id
            });
        }

        [Route("Balance")]
        public IHttpActionResult GetBalance()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
                return BadRequest();

            var result = _service.GetBalance(user.Id);

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            return Ok(result.Value);
        }

        // POST api/BankAccount/CreateBankAccount
        // Create bank account
        [Route("CreateBankAccount"),HttpPost]
        [AllowAnonymous]
        public IHttpActionResult CreateBankAccount([FromBody] RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var newaccount = new BankAccount()
                {
                    AccountBalance = 0,
                    UserName = model.UserName
                };
                IdentityResult result = UserManager.Create(newaccount, model.Password);

                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST api/BankAccount/Deposit
        //
        [Route("Deposit")]
        [HttpPost]
        public IHttpActionResult Deposit([FromBody]decimal amount)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
                return BadRequest();

            var result = _service.Deposit(user.Id, amount);

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(result.Message);
        }

        [Route("Userstatements")]
        public IHttpActionResult GetUserStatements()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if(user==null)
                return BadRequest();

            var result = _service.GetUserStatement(user.Id);
            
            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(from item in result.Value select new TransactionViewModel()
                {
                    Username = item.Username,
                    Amount = item.Amount,
                    TransferId = item.TransferId
                });
        }

        [Route("Withdraw")]
        [HttpPost]
        public IHttpActionResult Withdraw([FromBody] decimal amount)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
                return BadRequest();

            var result = _service.Withdraw(user.Id, amount);

            if (result.Status!=BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(result.Message);
        }

        [Route("Transfer")]
        [HttpPost]

        public IHttpActionResult Transfer([FromBody]TransferViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
                return BadRequest();

            var result = _service.Transfer(user.Id, model.DestanationId, model.Amount);

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(result.Message);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}