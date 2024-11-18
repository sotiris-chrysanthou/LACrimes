using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Weapon>> GetAll() {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Weapons
                .Include(w => w.CrimeRecords)
                .ToListAsync();
        }

        public async Task<Weapon?> GetById(Guid id) {
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
