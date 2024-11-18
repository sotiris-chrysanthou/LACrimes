using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class WeaponRepoTests {

        [Fact]
        public async Task CanAddAndRetrieveWeapon() {
            ;
            var repo = new WeaponRepo(true);

            var weapon = new Weapon { ID = Guid.NewGuid(), Code = 123, Desc = "Test Weapon" };
            await repo.Add(weapon);

            var retrievedWeapon = await repo.GetById(weapon.ID);

            Assert.NotNull(retrievedWeapon);
            Assert.Equal("Test Weapon", retrievedWeapon!.Desc);
        }

        [Fact]
        public async Task CanUpdateWeapon() {
            ;
            var repo = new WeaponRepo(true);

            var weaponId = Guid.NewGuid();
            var weapon = new Weapon { ID = weaponId, Code = 123, Desc = "Test Weapon" };
            await repo.Add(weapon);

            var updatedWeapon = new Weapon { ID = weaponId, Code = 456, Desc = "Updated Weapon" };
            await repo.Update(weaponId, updatedWeapon);

            var retrievedWeapon = await repo.GetById(weaponId);

            Assert.NotNull(retrievedWeapon);
            Assert.Equal("Updated Weapon", retrievedWeapon!.Desc);
            Assert.Equal(456, retrievedWeapon.Code);
        }

        [Fact]
        public async Task CanDeleteWeapon() {
            ;
            var repo = new WeaponRepo(true);

            var weaponId = Guid.NewGuid();
            var weapon = new Weapon { ID = weaponId, Code = 123, Desc = "Test Weapon" };
            await repo.Add(weapon);

            await repo.Delete(weaponId);

            var retrievedWeapon = await repo.GetById(weaponId);

            Assert.Null(retrievedWeapon);
        }

        [Fact]
        public async Task CanGetAllWeapons() {
            ;
            var repo = new WeaponRepo(true);

            var weapon1 = new Weapon { ID = Guid.NewGuid(), Code = 123, Desc = "Test Weapon 1" };
            var weapon2 = new Weapon { ID = Guid.NewGuid(), Code = 456, Desc = "Test Weapon 2" };
            await repo.Add(weapon1);
            await repo.Add(weapon2);

            var weapons = await repo.GetAll();

            Assert.Equal(2, weapons.Count);
            Assert.Contains(weapons, w => w.Desc == "Test Weapon 1");
            Assert.Contains(weapons, w => w.Desc == "Test Weapon 2");
        }
    }
}