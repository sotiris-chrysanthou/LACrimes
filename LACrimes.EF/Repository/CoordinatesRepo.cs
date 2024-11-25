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
    public class CoordinatesRepo : IEntityRepo<Coordinates> {
        private readonly bool _onlyForTest;

        public CoordinatesRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(Coordinates entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCoordinates = await context.Coordinates
                .Where(c => c.ID == id)
                .SingleOrDefaultAsync();
            if(dbCoordinates == null) {
                throw new Exception($"Coordinates with id: {id} not found");
            }
            context.Remove(dbCoordinates);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Coordinates>> GetAll(Expression<Func<Coordinates, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = c => false; // Is false because I don't want to return all records by default. Too many records
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.Coordinates
                    .Include(c => c.CrimeRecords)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.Coordinates
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Coordinates?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.Coordinates
                .Where(c => c.ID == id)
                .Include(c => c.CrimeRecords)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, Coordinates entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCoordinates = await context.Coordinates
                .Where(c => c.ID == id)
                .SingleOrDefaultAsync();
            if(dbCoordinates == null) {
                throw new Exception($"Coordinates with id: {id} not found");
            }
            dbCoordinates.Lat = entity.Lat;
            dbCoordinates.Lon = entity.Lon;
            await context.SaveChangesAsync();
        }
    }
}
