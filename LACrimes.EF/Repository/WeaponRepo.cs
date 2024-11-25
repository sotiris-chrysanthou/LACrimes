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
    public class WeaponRepo : IEntityRepo<Weapon> {
        private readonly bool _onlyForTest;

        public WeaponRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Weapon entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbWeapon = await context.Weapons
                .Where(w => w.ID == id)
                .SingleOrDefaultAsync();
            if(dbWeapon == null) {
                throw new Exception($"Weapon with id: {id} not found");
            }
            context.Remove(dbWeapon);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Weapon>> GetAll(Expression<Func<Weapon, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = w => false; // Is false because I don't want to return all records by default. Too many records
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.Weapons
                    .Include(w => w.CrimeRecords)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.Weapons
                .Include(w => w.CrimeRecords)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Weapon?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Weapons
                .Where(w => w.ID == id)
                .Include(w => w.CrimeRecords)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Weapon entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbWeapon = await context.Weapons
                .Where(w => w.ID == id)
                .SingleOrDefaultAsync();
            if(dbWeapon == null) {
                throw new Exception($"Weapon with id: {id} not found");
            }
            dbWeapon.Code = entity.Code;
            dbWeapon.Desc = entity.Desc;
            await context.SaveChangesAsync();
        }
    }
}
