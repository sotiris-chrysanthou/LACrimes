using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class PremisRepoTests {

        [Fact]
        public async Task CanAddAndRetrievePremis() {
            ;
            var repo = new PremisRepo(true);

            var premis = new Premis { ID = Guid.NewGuid(), Code = 123, Desc = "Test Premis" };
            await repo.Add(premis);

            var retrievedPremis = await repo.GetById(premis.ID);

            Assert.NotNull(retrievedPremis);
            Assert.Equal("Test Premis", retrievedPremis!.Desc);
        }

        [Fact]
        public async Task CanUpdatePremis() {
            ;
            var repo = new PremisRepo(true);

            var premisId = Guid.NewGuid();
            var premis = new Premis { ID = premisId, Code = 123, Desc = "Test Premis" };
            await repo.Add(premis);

            var updatedPremis = new Premis { ID = premisId, Code = 456, Desc = "Updated Premis" };
            await repo.Update(premisId, updatedPremis);

            var retrievedPremis = await repo.GetById(premisId);

            Assert.NotNull(retrievedPremis);
            Assert.Equal("Updated Premis", retrievedPremis!.Desc);
            Assert.Equal(456, retrievedPremis.Code);
        }

        [Fact]
        public async Task CanDeletePremis() {
            ;
            var repo = new PremisRepo(true);

            var premisId = Guid.NewGuid();
            var premis = new Premis { ID = premisId, Code = 123, Desc = "Test Premis" };
            await repo.Add(premis);

            await repo.Delete(premisId);

            var retrievedPremis = await repo.GetById(premisId);

            Assert.Null(retrievedPremis);
        }

        [Fact]
        public async Task CanGetAllPremis() {
            ;
            var repo = new PremisRepo(true);

            var premis1 = new Premis { ID = Guid.NewGuid(), Code = 123, Desc = "Test Premis 1" };
            var premis2 = new Premis { ID = Guid.NewGuid(), Code = 456, Desc = "Test Premis 2" };
            await repo.Add(premis1);
            await repo.Add(premis2);

            var premisList = await repo.GetAll();

            Assert.Equal(2, premisList.Count);
            Assert.Contains(premisList, p => p.Desc == "Test Premis 1");
            Assert.Contains(premisList, p => p.Desc == "Test Premis 2");
        }
    }
}
