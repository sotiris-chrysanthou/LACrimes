using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace LACrimes.Tests.EF {
    public class StatusRepoTests {

        [Fact]
        public async Task CanAddAndRetrieveStatus() {
            ;
            var repo = new StatusRepo(true);

            var status = new Status { ID = Guid.NewGuid(), Code = "A", Desc = "Test Status" };
            await repo.Add(status);

            var retrievedStatus = await repo.GetById(status.ID);

            Assert.NotNull(retrievedStatus);
            Assert.Equal("Test Status", retrievedStatus!.Desc);
        }

        [Fact]
        public async Task CanUpdateStatus() {
            ;
            var repo = new StatusRepo(true);

            var statusId = Guid.NewGuid();
            var status = new Status { ID = statusId, Code = "A", Desc = "Test Status" };
            await repo.Add(status);

            var updatedStatus = new Status { ID = statusId, Code = "B", Desc = "Updated Status" };
            await repo.Update(statusId, updatedStatus);

            var retrievedStatus = await repo.GetById(statusId);

            Assert.NotNull(retrievedStatus);
            Assert.Equal("Updated Status", retrievedStatus!.Desc);
            Assert.Equal("B", retrievedStatus.Code);
        }

        [Fact]
        public async Task CanDeleteStatus() {
            ;
            var repo = new StatusRepo(true);

            var statusId = Guid.NewGuid();
            var status = new Status { ID = statusId, Code = "A", Desc = "Test Status" };
            await repo.Add(status);

            await repo.Delete(statusId);

            var retrievedStatus = await repo.GetById(statusId);

            Assert.Null(retrievedStatus);
        }

        [Fact]
        public async Task CanGetAllStatuses() {
            ;
            var repo = new StatusRepo(true);

            var status1 = new Status { ID = Guid.NewGuid(), Code = "A", Desc = "Test Status 1" };
            var status2 = new Status { ID = Guid.NewGuid(), Code = "B", Desc = "Test Status 2" };
            await repo.Add(status1);
            await repo.Add(status2);

            var statuses = await repo.GetAll();

            Assert.Equal(2, statuses.Count);
            Assert.Contains(statuses, s => s.Desc == "Test Status 1");
            Assert.Contains(statuses, s => s.Desc == "Test Status 2");
        }
    }
}
