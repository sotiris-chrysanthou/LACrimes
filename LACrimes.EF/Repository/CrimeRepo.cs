using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Crime>> GetAll() {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Crimes
                .Include(c => c.CrimeRecordsCrime1)
                .Include(c => c.CrimeRecordsCrime2)
                .Include(c => c.CrimeRecordsCrime3)
                .ToListAsync();
        }

        public async Task<Crime?> GetById(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Crimes
                .Where(c => c.ID == id)
                .Include(c => c.CrimeRecordsCrime1)
                .Include(c => c.CrimeRecordsCrime2)
                .Include(c => c.CrimeRecordsCrime3)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Crime entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCrime = await context.Crimes
                .Where(a => a.ID == id)
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
