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
    public class StatusRepo : IEntityRepo<Status> {
        private readonly bool _onlyForTest;

        public StatusRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Status entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbStatus = await context.Statuses
                .Where(s => s.ID == id)
                .SingleOrDefaultAsync();
            if(dbStatus == null) {
                throw new Exception($"Status with id: {id} not found");
            }
            context.Remove(dbStatus);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Status>> GetAll(Expression<Func<Status, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = s => false; // Is false because I don't want to return all records by default. Too many records
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.Statuses
                    .Include(s => s.CrimeRecords)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.Statuses
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Status?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Statuses
                .Where(s => s.ID == id)
                .Include(s => s.CrimeRecords)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Status entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbStatus = await context.Statuses
                .Where(s => s.ID == id)
                .SingleOrDefaultAsync();
            if(dbStatus == null) {
                throw new Exception($"Status with id: {id} not found");
            }
            dbStatus.Code = entity.Code;
            dbStatus.Desc = entity.Desc;
            await context.SaveChangesAsync();
        }
    }
}
