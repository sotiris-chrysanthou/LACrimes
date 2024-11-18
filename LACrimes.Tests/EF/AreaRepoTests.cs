using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class AreaRepoTests {
        [Fact]
        public async Task CanAddAndRetrieveArea() {
            var repo = new AreaRepo(true);

            var area = new Area { ID = Guid.NewGuid(), Code = 1, Name = "Test Area" };
            await repo.Add(area);

            var retrievedArea = await repo.GetById(area.ID);

            Assert.NotNull(retrievedArea);
            Assert.Equal("Test Area", retrievedArea!.Name);
        }

        [Fact]
        public async Task CanUpdateArea() {
            var repo = new AreaRepo(true);

            var areaId = Guid.NewGuid();
            var area = new Area { ID = areaId, Code = 1, Name = "Test Area" };
            await repo.Add(area);

            var updatedArea = new Area { ID = areaId, Code = 2, Name = "Updated Area" };
            await repo.Update(areaId, updatedArea);

            var retrievedArea = await repo.GetById(areaId);

            Assert.NotNull(retrievedArea);
            Assert.Equal("Updated Area", retrievedArea!.Name);
            Assert.Equal(2, retrievedArea.Code);
        }

        [Fact]
        public async Task CanDeleteArea() {
            var repo = new AreaRepo(true);

            var areaId = Guid.NewGuid();
            var area = new Area { ID = areaId, Code = 1, Name = "Test Area" };
            await repo.Add(area);

            await repo.Delete(areaId);

            var retrievedArea = await repo.GetById(areaId);

            Assert.Null(retrievedArea);
        }

        [Fact]
        public async Task CanGetAllAreas() {
            var repo = new AreaRepo(true);
            IList<Area> areas = await repo.GetAll();
            Assert.Empty(areas);

            var area1 = new Area { ID = Guid.NewGuid(), Code = 1, Name = "Test Area 1" };
            var area2 = new Area { ID = Guid.NewGuid(), Code = 2, Name = "Test Area 2" };
            await repo.Add(area1);
            await repo.Add(area2);

            areas = await repo.GetAll();

            Assert.Equal(2, areas.Count);
            Assert.Contains(areas, a => a.Name == "Test Area 1");
            Assert.Contains(areas, a => a.Name == "Test Area 2");
        }
    }
}