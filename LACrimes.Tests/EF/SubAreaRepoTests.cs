using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class SubAreaRepoTests {

        [Fact]
        public async Task CanAddAndRetrieveSubArea() {
            ;
            var repo = new SubAreaRepo(true);

            var subArea = new SubArea { ID = Guid.NewGuid(), RpdDistNo = "101", AreaID = Guid.NewGuid() };
            await repo.Add(subArea);

            var retrievedSubArea = await repo.GetById(subArea.ID);

            Assert.NotNull(retrievedSubArea);
            Assert.Equal("101", retrievedSubArea!.RpdDistNo);
        }

        [Fact]
        public async Task CanUpdateSubArea() {
            ;
            var repo = new SubAreaRepo(true);

            var subAreaId = Guid.NewGuid();
            var subArea = new SubArea { ID = subAreaId, RpdDistNo = "101", AreaID = Guid.NewGuid() };
            await repo.Add(subArea);

            var updatedSubArea = new SubArea { ID = subAreaId, RpdDistNo = "102", AreaID = subArea.AreaID };
            await repo.Update(subAreaId, updatedSubArea);

            var retrievedSubArea = await repo.GetById(subAreaId);

            Assert.NotNull(retrievedSubArea);
            Assert.Equal("102", retrievedSubArea!.RpdDistNo);
        }

        [Fact]
        public async Task CanDeleteSubArea() {
            ;
            var repo = new SubAreaRepo(true);

            var subAreaId = Guid.NewGuid();
            var subArea = new SubArea { ID = subAreaId, RpdDistNo = "101", AreaID = Guid.NewGuid() };
            await repo.Add(subArea);

            await repo.Delete(subAreaId);

            var retrievedSubArea = await repo.GetById(subAreaId);

            Assert.Null(retrievedSubArea);
        }

        [Fact]
        public async Task CanGetAllSubAreas() {
            ;
            var repo = new SubAreaRepo(true);

            var subArea1 = new SubArea { ID = Guid.NewGuid(), RpdDistNo = "101", AreaID = Guid.NewGuid() };
            var subArea2 = new SubArea { ID = Guid.NewGuid(), RpdDistNo = "102", AreaID = Guid.NewGuid() };
            await repo.Add(subArea1);
            await repo.Add(subArea2);

            var subAreas = await repo.GetAll();

            Assert.Equal(2, subAreas.Count);
            Assert.Contains(subAreas, sa => sa.RpdDistNo == "101");
            Assert.Contains(subAreas, sa => sa.RpdDistNo == "102");
        }
    }
}