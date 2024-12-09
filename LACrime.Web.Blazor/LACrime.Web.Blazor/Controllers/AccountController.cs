using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LACrimes.Web.Blazor.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        // Properties
        private readonly IEntityRepo<Account> _accountRepo;
        private String _errorMessage;

        // Constructors
        public AccountController(IEntityRepo<Account> accountRepo) {
            _accountRepo = accountRepo;
            _errorMessage = String.Empty; ;
        }

        // GET: api/<AccountController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<AccountDto>?> Get() {
            try {
                var result = await _accountRepo.GetAll();
                var selectAccountList = result.Select(a => new AccountDto(a)).ToList();
                return selectAccountList;
            } catch(DbException) {
                return null;
            }
        }
        // POST api/<AcountController>
        [HttpPost]
        public async Task<ActionResult> Post(AccountDto newAccount) {

            var accounts = await _accountRepo.GetAll();
            if(accounts.FirstOrDefault(a => a.Username == newAccount.Username) == null) {

                var dbAccount = new Account(newAccount.ID,
                                            newAccount.Username,
                                            newAccount.Password,
                                            newAccount.Role);
                try {
                    await _accountRepo.Add(dbAccount);
                    return Ok();
                } catch(DbException ex) {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(_errorMessage);
        }

        // PUT api/<AccountController>/5
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(AccountDto account) {
            var accounts = await _accountRepo.GetAll(e => e.ID == account.ID);
            var dbAccount = accounts.SingleOrDefault(e => e.ID == account.ID);
            if(dbAccount == null) {
                return BadRequest($"Account not found");
            }

            dbAccount.Username = account.Username;
            dbAccount.Password = account.Password;
            dbAccount.Role = account.Role;
            try {
                await _accountRepo.Update(account.ID, dbAccount);
            } catch(DbUpdateException ex) {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id) {
            try {
                await _accountRepo.Delete(id);
            } catch(KeyNotFoundException) {
                return BadRequest($"Account not found");
            }
            return Ok();
        }
    }
}
