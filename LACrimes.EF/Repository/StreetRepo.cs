using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Street>> GetAll() {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Streets
                .Include(s => s.CrimeRecordsStreet)
                .Include(s => s.CrimeRecordsCrossStreet)
                .ToListAsync();
        }

        public async Task<Street?> GetById(Guid id) {
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
