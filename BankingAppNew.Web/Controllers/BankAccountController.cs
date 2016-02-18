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
        IBankService service;
        public BankUserManager UserManager { get; private set; }
        //User.Identity.GetUserId()

        public BankAccountController(IBankService service)
        {
            this.service = service;
            UserManager = Startup.UserManagerFactory();
        }
        // GET api/BankAccount/AccountList
        // Получить список
        [Route("AccountList")]
        public IHttpActionResult GetAccountList()
        {
            var result = service.AccountList();
            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            return Ok(service.AccountList().Value.AsEnumerable());
        }

        [Route("Balance/{id}")]
        public IHttpActionResult GetBalance(long id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var result = service.GetBalance(user.Id);

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            return Ok(result.Value);
        }

        // POST api/BankAccount/CreateBankAccount
        // Create bank account
        [Route("CreateBankAccount"),HttpPost]
        public IHttpActionResult CreateBankAccount([FromBody] CreateAccountViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var result = service.CreateAccount(new BankAccount()
            {
                AccountBalance = model.Balance,
                UserName = model.UserName,
                Email = model.Email
            });

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(result.Message);
        }

        // PUT api/BankAccount/Deposit
        //
        [Route("deposit")]
        [HttpPost]
        public IHttpActionResult Deposit([FromBody]decimal amount)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var result = service.Deposit(user.Id, amount);

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(result.Message);
        }

        [Route("userstatements")]
        public IHttpActionResult GetUserStatements()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var result = service.GetUserStatement(user.Id);

            if (result.Status != BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(result.Value);
        } 

        [Route("withdraw")]
        [HttpPost]
        public IHttpActionResult GetWithdraw([FromBody] decimal amount)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var result = service.Withdraw(user.Id, amount);

            if (result.Status!=BankRequestStatus.Done)
                return BadRequest(result.Message);
            else
                return Ok(result.Message);
        }

        [Route("transfer")]
        [HttpPost]

        public IHttpActionResult Transfer([FromBody]TransferViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var result = service.Transfer(user.Id, model.DestanationId, model.Amount);

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