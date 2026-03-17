using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OscarIoT.Api.Data;
using OscarIoT.Api.DTOs;
using OscarIoT.Api.Models;

[ApiController]
[Route("api/v1/[controller]")]

public class SensorReadingsController : ControllerBase
{
    private readonly AppDbContext _db;

    public SensorReadingsController(AppDbContext db) // This constructor takes an instance of AppDbContext as a parameter, which is injected by the dependency injection system. The AppDbContext is used to interact with the database and perform CRUD operations on the SensorReadings table.
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SensorReading>>> GetSensorReadings()  // This action method handles GET requests to retrieve all sensor readings from the database. It uses Entity Framework Core to asynchronously query the SensorReadings table, ordering the results by timestamp in descending order (most recent first). The results are returned as an HTTP 200 OK response with the list of sensor readings in the response body.
    {
        var readings = await _db.SensorReadings
            .OrderByDescending(r => r.Timestamp)
            .ToListAsync();

        return Ok(readings);
    }

    [HttpGet("{id}")] // This action method handles GET requests to retrieve a specific sensor reading by its unique identifier (id). It uses Entity Framework Core to asynchronously find the sensor reading with the specified id in the SensorReadings table. If the reading is not found, it returns an HTTP 404 Not Found response. If the reading is found, it returns an HTTP 200 OK response with the sensor reading in the response body.

    public async Task<ActionResult<SensorReading>> GetById(int id) // This action method handles GET requests to retrieve a specific sensor reading by its unique identifier (id). It uses Entity Framework Core to asynchronously find the sensor reading with the specified id in the SensorReadings table. If the reading is not found, it returns an HTTP 404 Not Found response. If the reading is found, it returns an HTTP 200 OK response with the sensor reading in the response body.
    {
        var reading = await _db.SensorReadings.FindAsync(id);

        if (reading == null)

            return NotFound();

        return Ok(reading);
    }

    [HttpGet("sensor/{sensorId}")]
    public async Task<ActionResult<IEnumerable<SensorReading>>> GetBySensor(string sensorId) // This action method handles GET requests to retrieve all sensor readings for a specific sensor identified by its SensorId. It uses Entity Framework Core to asynchronously query the SensorReadings table, filtering the results by the specified SensorId and ordering them by timestamp in descending order. The results are returned as an HTTP 200 OK response with the list of sensor readings in the response body.
    {
        var readings = await _db.SensorReadings
            .Where(r => r.SensorId == sensorId)
            .OrderByDescending(r => r.Timestamp)
            .ToListAsync();

        return Ok(readings);
    }

    [HttpPost]

    public async Task<ActionResult<SensorReading>> Create(CreateSensorReadingDto dto) // This action method handles POST requests to create a new sensor reading. It takes a CreateSensorReadingDto object as input, which contains the necessary information to create a new sensor reading (SensorId, SensorType, Value, Unit, and Location). The method creates a new SensorReading object using the data from the DTO and sets the Timestamp to the current time. It then adds the new sensor reading to the database context and saves the changes asynchronously. Finally, it returns an HTTP 201 Created response with the location of the newly created resource and the sensor reading in the response body.
    {
        var reading = new SensorReading
        {
            SensorId = dto.SensorId,
            SensorType = dto.SensorType,
            Value = dto.Value,
            Unit = dto.Unit,
            Location = dto.Location,
            Timestamp = DateTime.UtcNow,
        };

        _db.SensorReadings.Add(reading);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = reading.Id }, reading);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id) // This action method handles DELETE requests to delete a specific sensor reading by its unique identifier (id). It uses Entity Framework Core to asynchronously find the sensor reading with the specified id in the SensorReadings table. If the reading is not found, it returns an HTTP 404 Not Found response. If the reading is found, it removes the sensor reading from the database context and saves the changes asynchronously. Finally, it returns an HTTP 204 No Content response to indicate that the deletion was successful and there is no content to return in the response body.
    {
        var reading = await _db.SensorReadings.FindAsync(id);

        if (reading == null)
            return NotFound();

        _db.SensorReadings.Remove(reading);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}


