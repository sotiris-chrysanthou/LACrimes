using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Status>> GetAll() {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Statuses
                .Include(s => s.CrimeRecords)
                .ToListAsync();
        }

        public async Task<Status?> GetById(Guid id) {
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
