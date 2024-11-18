using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.EF.Repository {
    public class SubAreaRepo : IEntityRepo<SubArea> {
        private readonly bool _onlyForTest;

        public SubAreaRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(SubArea entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbSubArea = await context.SubAreas
                .Where(sa => sa.ID == id)
                .SingleOrDefaultAsync();
            if(dbSubArea == null) {
                throw new Exception($"SubArea with id: {id} not found");
            }
            context.Remove(dbSubArea);
            await context.SaveChangesAsync();
        }

        public async Task<IList<SubArea>> GetAll() {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.SubAreas
                .Include(sa => sa.CrimeRecords)
                .ToListAsync();
        }

        public async Task<SubArea?> GetById(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.SubAreas
                .Where(a => a.ID == id)
                .Include(sa => sa.CrimeRecords)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, SubArea entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbArea = await context.SubAreas
                .Where(sa => sa.ID == id)
                .SingleOrDefaultAsync();
            if(dbArea == null) {
                throw new Exception($"SubArea with id: {id} not found");
            }
            dbArea.RpdDistNo = entity.RpdDistNo;
            dbArea.AreaID = entity.AreaID;
            await context.SaveChangesAsync();
        }
    }
}
