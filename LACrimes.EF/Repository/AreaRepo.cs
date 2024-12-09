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
    public class AreaRepo : IEntityRepo<Area> {
        private readonly bool _onlyForTest;

        public AreaRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Area entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbArea = await context.Areas
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();
            if(dbArea == null) {
                throw new Exception($"Area with id: {id} not found");
            }
            context.Remove(dbArea);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Area>> GetAll(Expression<Func<Area, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = a => true; // Is true because i want to return all records by default
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.Areas
                    .Include(a => a.SubAreas).ThenInclude(sa => sa.CrimeRecords)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.Areas
                //.Include(a => a.SubAreas)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Area?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Areas
                .Where(a => a.ID == id)
                .Include(a => a.SubAreas).ThenInclude(sa => sa.CrimeRecords)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Area entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbArea = await context.Areas
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();
            if(dbArea == null) {
                throw new Exception($"Area with id: {id} not found");
            }
            dbArea.Code = entity.Code;
            dbArea.Name = entity.Name;
            await context.SaveChangesAsync();
        }
    }
}
