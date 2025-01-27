﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.EF.Repository {
    public class PremisRepo : IEntityRepo<Premis> {
        private readonly bool _onlyForTest;

        public PremisRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Premis entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbPremis = await context.Premis
                .Where(p => p.ID == id)
                .SingleOrDefaultAsync();
            if(dbPremis == null) {
                throw new Exception($"Premis with id: {id} not found");
            }
            context.Remove(dbPremis);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Premis>> GetAll(Expression<Func<Premis, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = p => true; // Is true because I want to return all records by default.
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.Premis
                    .Include(p => p.CrimeRecords)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.Premis
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Premis?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Premis
                .Where(p => p.ID == id)
                .Include(p => p.CrimeRecords)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Premis entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbPremis = await context.Premis
                .Where(p => p.ID == id)
                .SingleOrDefaultAsync();
            if(dbPremis == null) {
                throw new Exception($"Premis with id: {id} not found");
            }
            dbPremis.Code = entity.Code;
            dbPremis.Desc = entity.Desc;
            await context.SaveChangesAsync();
        }
    }
}
