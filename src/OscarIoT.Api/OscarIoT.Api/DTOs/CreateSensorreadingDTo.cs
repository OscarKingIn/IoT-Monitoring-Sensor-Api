using System.ComponentModel.DataAnnotations; // This using directive is necessary to use the data annotation attributes like [Required] and [Range] for validating the properties of the CreateSensorReadingDto class.

namespace OscarIoT.Api.DTOs;

public class CreateSensorReadingDto // This DTO (Data Transfer Object) is used for creating a new sensor reading. It includes properties for the sensor's unique identifier, type, value, unit of measurement, and location. The Timestamp property is not included here because it will be set automatically to the current time when the sensor reading is created.
{
    [Required]
    public string SensorId { get; set; } = string.Empty;
    [Required]
    public string SensorType { get; set; } = string.Empty;
    [Required]
    [Range(-273.15, 10000)] // Assuming the value is a temperature in Celsius, it cannot be below absolute zero. Adjust the range as needed for other types of sensors.
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
}