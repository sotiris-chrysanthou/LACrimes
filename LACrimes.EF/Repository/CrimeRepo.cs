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
    public class CrimeRepo : IEntityRepo<Crime> {
        private readonly bool _onlyForTest;

        public CrimeRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Crime entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCrime = await context.Crimes
                .Where(c => c.ID == id)
                .SingleOrDefaultAsync();
            if(dbCrime == null) {
                throw new Exception($"Crime with id: {id} not found");
            }
            context.Remove(dbCrime);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Crime>> GetAll(Expression<Func<Crime, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = c => true; // Is true because i want to return all records by default
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.Crimes
                    .Include(c => c.CrimeSeverities).ThenInclude(crmS => crmS.CrimeRecord)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.Crimes
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Crime?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Crimes
                .Where(c => c.ID == id)
                .Include(c => c.CrimeSeverities).ThenInclude(crmS => crmS.CrimeRecord)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Crime entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCrime = await context.Crimes
                .Where(c => c.ID == id)
                .SingleOrDefaultAsync();
            if(dbCrime == null) {
                throw new Exception($"Crime with id: {id} not found");
            }
            dbCrime.Code = entity.Code;
            dbCrime.Desc = entity.Desc;
            await context.SaveChangesAsync();
        }
    }
}
