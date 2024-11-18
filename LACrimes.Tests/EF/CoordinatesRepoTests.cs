using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class CoordinatesRepoTests {

        [Fact]
        public async Task CanAddAndRetrieveCoordinates() {
            ;
            var repo = new CoordinatesRepo(true);

            var coordinates = new Coordinates { ID = Guid.NewGuid(), Lat = 34.0522, Lon = -118.2437 };
            await repo.Add(coordinates);

            var retrievedCoordinates = await repo.GetById(coordinates.ID);

            Assert.NotNull(retrievedCoordinates);
            Assert.Equal(34.0522, retrievedCoordinates!.Lat);
            Assert.Equal(-118.2437, retrievedCoordinates.Lon);
        }

        [Fact]
        public async Task CanUpdateCoordinates() {
            ;
            var repo = new CoordinatesRepo(true);

            var coordinatesId = Guid.NewGuid();
            var coordinates = new Coordinates { ID = coordinatesId, Lat = 34.0522, Lon = -118.2437 };
            await repo.Add(coordinates);

            var updatedCoordinates = new Coordinates { ID = coordinatesId, Lat = 35.0000, Lon = -119.0000 };
            await repo.Update(coordinatesId, updatedCoordinates);

            var retrievedCoordinates = await repo.GetById(coordinatesId);

            Assert.NotNull(retrievedCoordinates);
            Assert.Equal(35.0000, retrievedCoordinates!.Lat);
            Assert.Equal(-119.0000, retrievedCoordinates.Lon);
        }

        [Fact]
        public async Task CanDeleteCoordinates() {
            ;
            var repo = new CoordinatesRepo(true);

            var coordinatesId = Guid.NewGuid();
            var coordinates = new Coordinates { ID = coordinatesId, Lat = 34.0522, Lon = -118.2437 };
            await repo.Add(coordinates);

            await repo.Delete(coordinatesId);

            var retrievedCoordinates = await repo.GetById(coordinatesId);

            Assert.Null(retrievedCoordinates);
        }

        [Fact]
        public async Task CanGetAllCoordinates() {
            ;
            var repo = new CoordinatesRepo(true);

            var coordinates1 = new Coordinates { ID = Guid.NewGuid(), Lat = 34.0522, Lon = -118.2437 };
            var coordinates2 = new Coordinates { ID = Guid.NewGuid(), Lat = 35.0000, Lon = -119.0000 };
            await repo.Add(coordinates1);
            await repo.Add(coordinates2);

            var coordinatesList = await repo.GetAll();

            Assert.Equal(2, coordinatesList.Count);
            Assert.Contains(coordinatesList, c => c.Lat == 34.0522);
            Assert.Contains(coordinatesList, c => c.Lat == 35.0000);
        }
    }
}