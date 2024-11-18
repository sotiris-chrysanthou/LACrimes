using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class StreetRepoTests {

        [Fact]
        public async Task CanAddAndRetrieveStreet() {
            ;
            var repo = new StreetRepo(true);

            var street = new Street { ID = Guid.NewGuid(), Name = "Test Street" };
            await repo.Add(street);

            var retrievedStreet = await repo.GetById(street.ID);

            Assert.NotNull(retrievedStreet);
            Assert.Equal("Test Street", retrievedStreet!.Name);
        }

        [Fact]
        public async Task CanUpdateStreet() {
            ;
            var repo = new StreetRepo(true);

            var streetId = Guid.NewGuid();
            var street = new Street { ID = streetId, Name = "Test Street" };
            await repo.Add(street);

            var updatedStreet = new Street { ID = streetId, Name = "Updated Street" };
            await repo.Update(streetId, updatedStreet);

            var retrievedStreet = await repo.GetById(streetId);

            Assert.NotNull(retrievedStreet);
            Assert.Equal("Updated Street", retrievedStreet!.Name);
        }

        [Fact]
        public async Task CanDeleteStreet() {
            ;
            var repo = new StreetRepo(true);

            var streetId = Guid.NewGuid();
            var street = new Street { ID = streetId, Name = "Test Street" };
            await repo.Add(street);

            await repo.Delete(streetId);

            var retrievedStreet = await repo.GetById(streetId);

            Assert.Null(retrievedStreet);
        }

        [Fact]
        public async Task CanGetAllStreets() {
            ;
            var repo = new StreetRepo(true);

            var street1 = new Street { ID = Guid.NewGuid(), Name = "Test Street 1" };
            var street2 = new Street { ID = Guid.NewGuid(), Name = "Test Street 2" };
            await repo.Add(street1);
            await repo.Add(street2);

            var streets = await repo.GetAll();

            Assert.Equal(2, streets.Count);
            Assert.Contains(streets, s => s.Name == "Test Street 1");
            Assert.Contains(streets, s => s.Name == "Test Street 2");
        }
    }
}