using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.EF.Repository {
    public class AccountRepo : IEntityRepo<Account> {
        private readonly bool _onlyForTest;

        public AccountRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Account entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbAccount = await context.Accounts
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();
            if(dbAccount == null) {
                throw new Exception($"Account with id: {id} not found");
            }
            context.Remove(dbAccount);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Account>> GetAll(Expression<Func<Account, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = a => true; // Is true because i want to return all records by default
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Accounts
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Account?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Accounts
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Account entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbAccount = await context.Accounts
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();
            if(dbAccount == null) {
                throw new Exception($"Area with id: {id} not found");
            }
            dbAccount.Username = entity.Username;
            dbAccount.Password = entity.Password;
            dbAccount.Role = entity.Role;
            await context.SaveChangesAsync();
        }
    }
}
