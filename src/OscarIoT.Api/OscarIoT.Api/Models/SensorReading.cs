namespace OscarIoT.Api.Models;

// This class represents a sensor reading in the Oscar IoT system. It includes properties for the sensor's unique identifier, type, value, unit of measurement, timestamp, and location. This model can be used to store and retrieve sensor readings from a database or to transfer data between different parts of the application.
public class SensorReading
{
    public int Id { get; set; }
    public string SensorId { get; set; } = string.Empty;
    public string SensorType {get; set; } = string.Empty;
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Location { get; set; } = string.Empty;

}