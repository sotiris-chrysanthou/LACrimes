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
    public class CrimeSeverityRepo : IEntityRepo<CrimeSeverity> {

        private readonly bool _onlyForTest;

        public CrimeSeverityRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(CrimeSeverity entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCrimeSeverity = await context.CrimeSeverities
                .Where(cs => cs.ID == id)
                .SingleOrDefaultAsync();
            if(dbCrimeSeverity == null) {
                throw new Exception($"CrimeSeverity with id: {id} not found");
            }
            context.Remove(dbCrimeSeverity);
            await context.SaveChangesAsync();
        }

        public async Task<IList<CrimeSeverity>> GetAll(Expression<Func<CrimeSeverity, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = cs => false; // Is false because I don't want to return all records by default. Too many records
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.CrimeSeverities
                    .Include(cs => cs.CrimeRecord)
                    .Include(cs => cs.Crime)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.CrimeSeverities
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<CrimeSeverity?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.CrimeSeverities
                .Where(cs => cs.ID == id)
                .Include(cs => cs.CrimeRecord)
                .Include(cs => cs.Crime)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, CrimeSeverity entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCrimeSeverity = await context.CrimeSeverities
                .Where(cs => cs.ID == id)
                .SingleOrDefaultAsync();
            if(dbCrimeSeverity == null) {
                throw new Exception($"CrimeSeverity with id: {id} not found");
            }
            dbCrimeSeverity.CrimeRecordID = entity.CrimeRecordID;
            dbCrimeSeverity.CrimeID = entity.CrimeID;
            dbCrimeSeverity.Severity = entity.Severity;
            await context.SaveChangesAsync();
        }
    }
}
