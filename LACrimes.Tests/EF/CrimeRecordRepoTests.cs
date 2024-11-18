using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace LACrimes.Tests.EF {
    public class CrimeRecordRepoTests {

        [Fact]
        public async Task CanAddAndRetrieveCrimeRecord() {
            
            var crimeRepo = new CrimeRepo(true);
            var crime1 = new Crime {
                ID = Guid.NewGuid(),
                Code = 123,
                Desc = "Theft"
            };
            var crime2 = new Crime {
                ID = Guid.NewGuid(),
                Code = 456,
                Desc = "Assault"
            };
            await crimeRepo.Add(crime1);
            await crimeRepo.Add(crime2);

            var coordinatesRepo = new CoordinatesRepo(true);
            var coordinates = new Coordinates {
                ID = Guid.NewGuid(),
                Lat = 34.0522,
                Lon = -118.2437
            };
            await coordinatesRepo.Add(coordinates);

            var crimeRecordRepo = new CrimeRecordRepo(true);
            var crimeRecord = new CrimeRecord {
                ID = Guid.NewGuid(),
                DrNo = 456,
                DateRptd = DateTime.Now,
                DateOcc = DateTime.Now,
                Crime1ID = crime1.ID,//Guid.NewGuid(),
                Crime2ID = crime2.ID,//Guid.NewGuid(),
                Crime3ID = null,//Guid.NewGuid(),
                StreetID = null,//Guid.NewGuid(),
                CrossStreetID = null,//Guid.NewGuid(),
                SubAreaID = null,//Guid.NewGuid(),
                VictimID = null,//Guid.NewGuid(),
                PremisID = null,//Guid.NewGuid(),
                StatusID = null,//Guid.NewGuid(),
                WeaponID = null,//Guid.NewGuid(),
                CoordinatesID = coordinates.ID,//Guid.NewGuid()
            };
            await crimeRecordRepo.Add(crimeRecord);

            var retrievedCrimeRecord = await crimeRecordRepo.GetById(crimeRecord.ID);

            Assert.NotNull(retrievedCrimeRecord);
            Assert.Equal(456, retrievedCrimeRecord!.DrNo);
        }

        [Fact]
        public async Task CanUpdateCrimeRecord() {
            var crimeRepo = new CrimeRepo(true);
            var crime1 = new Crime {
                ID = Guid.NewGuid(),
                Code = 123,
                Desc = "Theft"
            };
            var crime2 = new Crime {
                ID = Guid.NewGuid(),
                Code = 456,
                Desc = "Assault"
            };
            await crimeRepo.Add(crime1);
            await crimeRepo.Add(crime2);

            var coordinatesRepo = new CoordinatesRepo(true);
            var coordinates = new Coordinates {
                ID = Guid.NewGuid(),
                Lat = 34.0522,
                Lon = -118.2437
            };
            await coordinatesRepo.Add(coordinates);

            var crimeRecordRepo = new CrimeRecordRepo(true);
            var crimeRecord = new CrimeRecord {
                ID = Guid.NewGuid(),
                DrNo = 456,
                DateRptd = DateTime.Now,
                DateOcc = DateTime.Now,
                Crime1ID = crime1.ID,//Guid.NewGuid(),
                Crime2ID = crime2.ID,//Guid.NewGuid(),
                Crime3ID = null,//Guid.NewGuid(),
                StreetID = null,//Guid.NewGuid(),
                CrossStreetID = null,//Guid.NewGuid(),
                SubAreaID = null,//Guid.NewGuid(),
                VictimID = null,//Guid.NewGuid(),
                PremisID = null,//Guid.NewGuid(),
                StatusID = null,//Guid.NewGuid(),
                WeaponID = null,//Guid.NewGuid(),
                CoordinatesID = coordinates.ID,//Guid.NewGuid()
            };
            await crimeRecordRepo.Add(crimeRecord);

            var updatedCrimeRecord = new CrimeRecord {
                ID = crimeRecord.ID,
                DrNo = 789,
                DateRptd = DateTime.Now,
                DateOcc = DateTime.Now,
                Crime1ID = crimeRecord.Crime1ID,
                Crime2ID = crimeRecord.Crime2ID,
                Crime3ID = crimeRecord.Crime3ID,
                StreetID = crimeRecord.StreetID,
                CrossStreetID = crimeRecord.CrossStreetID,
                SubAreaID = crimeRecord.SubAreaID,
                VictimID = crimeRecord.VictimID,
                PremisID = crimeRecord.PremisID,
                StatusID = crimeRecord.StatusID,
                WeaponID = crimeRecord.WeaponID,
                CoordinatesID = crimeRecord.CoordinatesID
            };
            await crimeRecordRepo.Update(crimeRecord.ID, updatedCrimeRecord);

            var retrievedCrimeRecord = await crimeRecordRepo.GetById(crimeRecord.ID);

            Assert.NotNull(retrievedCrimeRecord);
            Assert.Equal(789, retrievedCrimeRecord!.DrNo);
        }

        [Fact]
        public async Task CanDeleteCrimeRecord() {
            var crimeRepo = new CrimeRepo(true);
            var crime1 = new Crime {
                ID = Guid.NewGuid(),
                Code = 123,
                Desc = "Theft"
            };
            var crime2 = new Crime {
                ID = Guid.NewGuid(),
                Code = 456,
                Desc = "Assault"
            };
            await crimeRepo.Add(crime1);
            await crimeRepo.Add(crime2);

            var coordinatesRepo = new CoordinatesRepo(true);
            var coordinates = new Coordinates {
                ID = Guid.NewGuid(),
                Lat = 34.0522,
                Lon = -118.2437
            };
            await coordinatesRepo.Add(coordinates);
            var crimeRecordRepo = new CrimeRecordRepo(true);
            var crimeRecord = new CrimeRecord {
                ID = Guid.NewGuid(),
                DrNo = 456,
                DateRptd = DateTime.Now,
                DateOcc = DateTime.Now,
                Crime1ID = crime1.ID,//Guid.NewGuid(),
                Crime2ID = crime2.ID,//Guid.NewGuid(),
                Crime3ID = null,//Guid.NewGuid(),
                StreetID = null,//Guid.NewGuid(),
                CrossStreetID = null,//Guid.NewGuid(),
                SubAreaID = null,//Guid.NewGuid(),
                VictimID = null,//Guid.NewGuid(),
                PremisID = null,//Guid.NewGuid(),
                StatusID = null,//Guid.NewGuid(),
                WeaponID = null,//Guid.NewGuid(),
                CoordinatesID = coordinates.ID,//Guid.NewGuid()
            };
            await crimeRecordRepo.Add(crimeRecord);

            await crimeRecordRepo.Delete(crimeRecord.ID);

            var retrievedCrimeRecord = await crimeRecordRepo.GetById(crimeRecord.ID);

            Assert.Null(retrievedCrimeRecord);
        }

        [Fact]
        public async Task CanGetAllCrimeRecords() {
            var crimeRepo = new CrimeRepo(true);
            var crime1 = new Crime {
                ID = Guid.NewGuid(),
                Code = 123,
                Desc = "Theft"
            };
            var crime2 = new Crime {
                ID = Guid.NewGuid(),
                Code = 456,
                Desc = "Assault"
            };
            await crimeRepo.Add(crime1);
            await crimeRepo.Add(crime2);

            var coordinatesRepo = new CoordinatesRepo(true);
            var coordinates = new Coordinates {
                ID = Guid.NewGuid(),
                Lat = 34.0522,
                Lon = -118.2437
            };
            await coordinatesRepo.Add(coordinates);
            var crimeRecord1 = new CrimeRecord {
                ID = Guid.NewGuid(),
                DrNo = 123,
                DateRptd = DateTime.Now,
                DateOcc = DateTime.Now,
                Crime1ID = crime1.ID,//Guid.NewGuid(),
                Crime2ID = crime2.ID,//Guid.NewGuid(),
                Crime3ID = null,//Guid.NewGuid(),
                StreetID = null,//Guid.NewGuid(),
                CrossStreetID = null,//Guid.NewGuid(),
                SubAreaID = null,//Guid.NewGuid(),
                VictimID = null,//Guid.NewGuid(),
                PremisID = null,//Guid.NewGuid(),
                StatusID = null,//Guid.NewGuid(),
                WeaponID = null,//Guid.NewGuid(),
                CoordinatesID = coordinates.ID,//Guid.NewGuid()
            };

            var crimeRecord2 = new CrimeRecord {
                ID = Guid.NewGuid(),
                DrNo = 456,
                DateRptd = DateTime.Now,
                DateOcc = DateTime.Now,
                Crime1ID = crime1.ID,//Guid.NewGuid(),
                Crime2ID = crime2.ID,//Guid.NewGuid(),
                Crime3ID = null,//Guid.NewGuid(),
                StreetID = null,//Guid.NewGuid(),
                CrossStreetID = null,//Guid.NewGuid(),
                SubAreaID = null,//Guid.NewGuid(),
                VictimID = null,//Guid.NewGuid(),
                PremisID = null,//Guid.NewGuid(),
                StatusID = null,//Guid.NewGuid(),
                WeaponID = null,//Guid.NewGuid(),
                CoordinatesID = coordinates.ID,//Guid.NewGuid()
            };
            var crimeRecordRepo = new CrimeRecordRepo(true);
            await crimeRecordRepo.Add(crimeRecord1);
            await crimeRecordRepo.Add(crimeRecord2);

            var crimeRecords = await crimeRecordRepo.GetAll();

            Assert.Equal(2, crimeRecords.Count);
            Assert.Contains(crimeRecords, cr => cr.DrNo == crimeRecord1.DrNo);
            Assert.Contains(crimeRecords, cr => cr.DrNo == crimeRecord2.DrNo);
        }
    }
}