using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<CrimeRecord>> GetAll() {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.CrimesRecords
                .Include(crmR => crmR.Coordinates)
                .Include(crmR => crmR.Crime1)
                .Include(crmR => crmR.Crime2)
                .Include(crmR => crmR.Crime3)
                .Include(crmR => crmR.Street)
                .Include(crmR => crmR.CrossStreet)
                .Include(crmR => crmR.Premis)
                .Include(crmR => crmR.Status)
                .Include(crmR => crmR.SubArea).ThenInclude(sa => sa.Area)
                .Include(crmR => crmR.Victim)
                .Include(crmR => crmR.Weapon)
                .ToListAsync();
        }

        public async Task<CrimeRecord?> GetById(Guid id) {
            using var context = new LACrimeDbContext(_onlyForTest);
            return await context.CrimesRecords
                .Where(crmR => crmR.ID == id)
                .Include(crmR => crmR.Coordinates)
                .Include(crmR => crmR.Crime1)
                .Include(crmR => crmR.Crime2)
                .Include(crmR => crmR.Crime3)
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
                throw new Exception($"Area with id: {id} not found");
            }
            dbCrimeRecord.DrNo = entity.DrNo;
            dbCrimeRecord.DateOcc = entity.DateOcc;
            dbCrimeRecord.DateRptd = entity.DateRptd;

            dbCrimeRecord.CoordinatesID = entity.CoordinatesID;
            dbCrimeRecord.Crime1ID = entity.Crime1ID;
            dbCrimeRecord.Crime2ID = entity.Crime2ID;
            dbCrimeRecord.Crime3ID = entity.Crime3ID;
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
