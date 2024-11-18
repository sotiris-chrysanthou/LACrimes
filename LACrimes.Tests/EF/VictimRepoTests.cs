using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class VictimRepoTests {


        [Fact]
        public async Task CanAddAndRetrieveVictim() {
            ;
            var repo = new VictimRepo(true);

            var victim = new Victim { ID = Guid.NewGuid(), Age = 30, Sex = 'M', Descent = 'W' };
            await repo.Add(victim);

            var retrievedVictim = await repo.GetById(victim.ID);

            Assert.NotNull(retrievedVictim);
            Assert.Equal(30, retrievedVictim!.Age);
            Assert.Equal('M', retrievedVictim.Sex);
            Assert.Equal('W', retrievedVictim.Descent);
        }

        [Fact]
        public async Task CanUpdateVictim() {
            ;
            var repo = new VictimRepo(true);

            var victimId = Guid.NewGuid();
            var victim = new Victim { ID = victimId, Age = 30, Sex = 'M', Descent = 'W' };
            await repo.Add(victim);

            var updatedVictim = new Victim { ID = victimId, Age = 35, Sex = 'F', Descent = 'H' };
            await repo.Update(victimId, updatedVictim);

            var retrievedVictim = await repo.GetById(victimId);

            Assert.NotNull(retrievedVictim);
            Assert.Equal(35, retrievedVictim!.Age);
            Assert.Equal('F', retrievedVictim.Sex);
            Assert.Equal('H', retrievedVictim.Descent);
        }

        [Fact]
        public async Task CanDeleteVictim() {
            ;
            var repo = new VictimRepo(true);

            var victimId = Guid.NewGuid();
            var victim = new Victim { ID = victimId, Age = 30, Sex = 'M', Descent = 'W' };
            await repo.Add(victim);

            await repo.Delete(victimId);

            var retrievedVictim = await repo.GetById(victimId);

            Assert.Null(retrievedVictim);
        }

        [Fact]
        public async Task CanGetAllVictims() {
            ;
            var repo = new VictimRepo(true);

            var victim1 = new Victim { ID = Guid.NewGuid(), Age = 30, Sex = 'M', Descent = 'W' };
            var victim2 = new Victim { ID = Guid.NewGuid(), Age = 25, Sex = 'F', Descent = 'H' };
            await repo.Add(victim1);
            await repo.Add(victim2);

            var victims = await repo.GetAll();

            Assert.Equal(2, victims.Count);
            Assert.Contains(victims, v => v.Age == 30);
            Assert.Contains(victims, v => v.Age == 25);
        }
    }
}