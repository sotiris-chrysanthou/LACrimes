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
    public class StreetRepo : IEntityRepo<Street> {
        private readonly bool _onlyForTest;

        public StreetRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Street entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbStreet = await context.Streets
                .Where(s => s.ID == id)
                .SingleOrDefaultAsync();
            if(dbStreet == null) {
                throw new Exception($"Street with id: {id} not found");
            }
            context.Remove(dbStreet);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Street>> GetAll(Expression<Func<Street, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = s => false; // Is false because I don't want to return all records by default. Too many records
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.Streets
                    .Include(s => s.CrimeRecordsStreet)
                    .Include(s => s.CrimeRecordsCrossStreet)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.Streets
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Street?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Streets
                .Where(s => s.ID == id)
                .Include(s => s.CrimeRecordsStreet)
                .Include(s => s.CrimeRecordsCrossStreet)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Street entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbStreet = await context.Streets
                .Where(s => s.ID == id)
                .SingleOrDefaultAsync();
            if(dbStreet == null) {
                throw new Exception($"Street with id: {id} not found");
            }
            dbStreet.Name = entity.Name;
            await context.SaveChangesAsync();
        }
    }
}
