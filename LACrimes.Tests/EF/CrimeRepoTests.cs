using System;
using System.Linq;
using System.Threading.Tasks;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LACrimes.Tests.EF {
    public class CrimeRepoTests {

    [Fact]
    public async Task CanAddAndRetrieveCrime() {
        ;
        var repo = new CrimeRepo(true);

        var crime = new Crime { ID = Guid.NewGuid(), Code = 123, Desc = "Test Crime" };
        await repo.Add(crime);

        var retrievedCrime = await repo.GetById(crime.ID);

        Assert.NotNull(retrievedCrime);
        Assert.Equal("Test Crime", retrievedCrime!.Desc);
    }

    [Fact]
    public async Task CanUpdateCrime() {
        ;
        var repo = new CrimeRepo(true);

        var crimeId = Guid.NewGuid();
        var crime = new Crime { ID = crimeId, Code = 123, Desc = "Test Crime" };
        await repo.Add(crime);

        var updatedCrime = new Crime { ID = crimeId, Code = 456, Desc = "Updated Crime" };
        await repo.Update(crimeId, updatedCrime);

        var retrievedCrime = await repo.GetById(crimeId);

        Assert.NotNull(retrievedCrime);
        Assert.Equal("Updated Crime", retrievedCrime!.Desc);
        Assert.Equal(456, retrievedCrime.Code);
    }

    [Fact]
    public async Task CanDeleteCrime() {
        ;
        var repo = new CrimeRepo(true);

        var crimeId = Guid.NewGuid();
        var crime = new Crime { ID = crimeId, Code = 123, Desc = "Test Crime" };
        await repo.Add(crime);

        await repo.Delete(crimeId);

        var retrievedCrime = await repo.GetById(crimeId);

        Assert.Null(retrievedCrime);
    }

    [Fact]
    public async Task CanGetAllCrimes() {
        ;
        var repo = new CrimeRepo(true);

        var crime1 = new Crime { ID = Guid.NewGuid(), Code = 123, Desc = "Test Crime 1" };
        var crime2 = new Crime { ID = Guid.NewGuid(), Code = 456, Desc = "Test Crime 2" };
        await repo.Add(crime1);
        await repo.Add(crime2);

        var crimes = await repo.GetAll();

        Assert.Equal(2, crimes.Count);
        Assert.Contains(crimes, c => c.Desc == "Test Crime 1");
        Assert.Contains(crimes, c => c.Desc == "Test Crime 2");
    }
}
}
