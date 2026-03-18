using Microsoft.EntityFrameworkCore;    
using OscarIoT.Api.Data;
using OscarIoT.Api.Models;
using OscarIoT.Api.DTOs;
using Xunit;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Data.Common;

public class SensorReadingsTests
{
    private AppDbContext GetFreshDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContetxt>()
            .useInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
    //TEST 1: CreateReading_ShouldPersistToDatabase
    [Fact]

    public async Task CreateReading_ShouldPersistToDatabase()
    {
        var db = GetFreshDbContext();
        var reading = new SensorReadingsTests
        {
            SensorId = "WTR-QLD-001",
            SensorType = "water-flow",
            Value = 42.7,
            Unit = "L/s",
            Location = "Brisbane-Main-Pipe",
            Timestamp = DateTime.UtcNow        
        };

        db.SensorReadings.Add(reading);
        await db.SaveChangesAsync();

        var saved = await db.SensorReadings.FirstOrDefaultAsync(r => r.SensorId == "WTR-QLD-001");
        Assert.NotNull(saved);  
        Assert.Equal(42.7, saved.Value);
        Assert.Equal("L/s", saved.Unit);
    }
    //TEST 2: GetBySensor_ShouldReturnOnlyMatchingReadings
    [Fact]

    public async Task GetBySensor_ShouldReturnOnlyMatchingReadings()
    {
        var db = GetFreshDbContext();
        db.SensorReadings.AddRange(

            new SensorReading{ SensorId = "SENS-A", Value = 10, Unit = "PSI", SensorType ="pressure", Location = "Site1"},
            new SensorReading{ SensorId = "SENS-A", Value = 12, Unit = "PSI", SensorType ="pressure", Location = "Site1"},
            new SensorReading{ SensorId = "SENS-B", Value = 5, Unit = "PSI", SensorType ="pressure", Location = "Site2"}
        );
        await db.SaveChangesAsync();

        var results = await db.SensorReadings
            .where(r => r.SensorId == "SENS-A")
            .ToListAsync();

        Assert.Equal(2, results.Count);
        Assert.All(results, r => Assert.Equal("SENS-A", r.SensorId));
    }

    //TEST 3 EMPTY DATABASE SHOULD RETURN EMPTY LIST
    [Fact]

    public async Task GetAll_WithNoData_ShouldReturnEmptyList()
    {
        var db = GetFreshDbContext();
        var readings = await db.SensorReadings.ToListAsync();
        Assert.Empty(readings);
    }
}





