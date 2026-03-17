using Microsoft.EntityFrameworkCore;
using OscarIoT.Api.Models;

namespace OscarIoT.Api.Data; // This namespace contains the data access layer for the Oscar IoT API, including the AppDbContext class which is responsible for managing the database connection and providing access to the SensorReadings table.

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // This constructor takes DbContextOptions as a parameter and passes it to the base class constructor. This allows for configuration of the database connection, such as specifying the connection string and database provider.
    { }

    public DbSet<SensorReading> SensorReadings { get; set; } // This property represents the SensorReadings table in the database. It allows for querying and saving instances of the SensorReading model to the database.

    protected override void OnModelCreating(ModelBuilder modelBuilder) // This method is used to configure the model and its relationships using the Fluent API. It is called when the model is being created and can be used to specify things like table names, column types, and relationships between entities.
    {
        modelBuilder.Entity<SensorReading>() // This line specifies that we are configuring the SensorReading entity.
            .Property(s => s.SensorId) //   This line specifies that we are configuring the SensorId property of the SensorReading entity.
            .HasMaxLength(100); // This configuration sets the maximum length of the SensorId property to 100 characters in the database. This helps to ensure data integrity and can improve performance by limiting the size of the data stored in this column.

        modelBuilder.Entity<SensorReading>()
            .HasIndex(s => s.Timestamp); // This configuration creates an index on the Timestamp property of the SensorReading entity. Indexes can improve query performance when filtering or sorting by the Timestamp column, especially as the number of records in the SensorReadings table grows.
    }
}