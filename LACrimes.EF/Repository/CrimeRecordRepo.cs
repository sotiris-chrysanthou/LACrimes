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
    public class CrimeRecordRepo : IEntityRepo<CrimeRecord> {
        private readonly bool _onlyForTest;

        public CrimeRecordRepo(bool onlyForTest = false) {
            _onlyForTest = onlyForTest;
        }

        public async Task Add(CrimeRecord entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCrimeRecord = await context.CrimesRecords
                .Where(crmR => crmR.ID == id)
                .SingleOrDefaultAsync();
            if(dbCrimeRecord == null) {
                throw new Exception($"Crime record with id: {id} not found");
            }
            context.Remove(dbCrimeRecord);
            await context.SaveChangesAsync();
        }

        public async Task<IList<CrimeRecord>> GetAll(Expression<Func<CrimeRecord, bool>>? predicate = null, bool IncludeAll = false) {
            if(predicate == null) {
                predicate = crmR => false; // Is false because i don't want to return all records by default. Too many records
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            if(IncludeAll) {
                return await context.CrimesRecords
                    .Include(crmR => crmR.Coordinates)
                    .Include(crmR => crmR.CrimeSeverities).ThenInclude(crmS => crmS.Crime)
                    .Include(crmR => crmR.Street)
                    .Include(crmR => crmR.CrossStreet)
                    .Include(crmR => crmR.Premis)
                    .Include(crmR => crmR.Status)
                    .Include(crmR => crmR.SubArea).ThenInclude(sa => sa.Area)
                    .Include(crmR => crmR.Victim)
                    .Include(crmR => crmR.Weapon)
                    .Where(predicate)
                    .ToListAsync();
            }
            return await context.CrimesRecords
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<CrimeRecord?> GetById(Guid id) {
            if(id == Guid.Empty) {
                return null;
            }
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.CrimesRecords
                .Where(crmR => crmR.ID == id)
                .Include(crmR => crmR.Coordinates)
                .Include(crmR => crmR.CrimeSeverities).ThenInclude(crmS => crmS.Crime)
                .Include(crmR => crmR.Street)
                .Include(crmR => crmR.CrossStreet)
                .Include(crmR => crmR.Premis)
                .Include(crmR => crmR.Status)
                .Include(crmR => crmR.SubArea).ThenInclude(sa => sa.Area)
                .Include(crmR => crmR.Victim)
                .Include(crmR => crmR.Weapon)
                .SingleOrDefaultAsync();
        }

        public async Task Update(Guid id, CrimeRecord entity) {
            using var context = new LACrimeDbContext(_onlyForTest);
            var dbCrimeRecord = await context.CrimesRecords
                .Where(crmR => crmR.ID == id)
                .SingleOrDefaultAsync();
            if(dbCrimeRecord == null) {
                throw new Exception($"Crime record with id: {id} not found");
            }
            dbCrimeRecord.DrNo = entity.DrNo;
            dbCrimeRecord.DateOcc = entity.DateOcc;
            dbCrimeRecord.DateRptd = entity.DateRptd;

            dbCrimeRecord.CoordinatesID = entity.CoordinatesID;
            dbCrimeRecord.StreetID = entity.StreetID;
            dbCrimeRecord.CrossStreetID = entity.CrossStreetID;
            dbCrimeRecord.PremisID = entity.PremisID;
            dbCrimeRecord.StatusID = entity.StatusID;
            dbCrimeRecord.SubAreaID = entity.SubAreaID;
            dbCrimeRecord.VictimID = entity.VictimID;
            dbCrimeRecord.WeaponID = entity.WeaponID;
            await context.SaveChangesAsync();
        }
    }
}
