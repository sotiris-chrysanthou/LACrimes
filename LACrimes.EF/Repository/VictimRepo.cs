﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.EF.Repository {
    public class VictimRepo : IEntityRepo<Victim> {
        private readonly bool _onlyForTest;

        public VictimRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Victim entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbVictim = await context.Victims
                .Where(v => v.ID == id)
                .SingleOrDefaultAsync();
            if(dbVictim == null) {
                throw new Exception($"Victim with id: {id} not found");
            }
            context.Remove(dbVictim);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Victim>> GetAll() {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Victims
                .Include(v => v.CrimeRecords)
                .ToListAsync();
        }

        public async Task<Victim?> GetById(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Victims
                .Where(v => v.ID == id)
                .Include(v => v.CrimeRecords)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Victim entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbVictim = await context.Victims
                .Where(v => v.ID == id)
                .SingleOrDefaultAsync();
            if(dbVictim == null) {
                throw new Exception($"Victim with id: {id} not found");
            }
            dbVictim.Age = entity.Age;
            dbVictim.Descent = entity.Descent;
            dbVictim.Sex = entity.Sex;
            await context.SaveChangesAsync();
        }
    }
}