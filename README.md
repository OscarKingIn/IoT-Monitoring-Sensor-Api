# OscarIoT - Sensor Monitoring API

A comprehensive RESTful API for managing and monitoring IoT sensor data. Built with .NET 10 and Entity Framework Core, OscarIoT provides real-time sensor reading collection, storage, and retrieval capabilities for distributed IoT deployments.

## Overview

OscarIoT is a production-ready backend service designed to aggregate, store, and expose sensor readings from distributed IoT devices. The system supports multiple sensor types, locations, and provides flexible querying capabilities for analytics and monitoring applications.

### Key Features

- RESTful API for sensor data management
- Multi-sensor support with type and location tracking
- In-memory database with Entity Framework Core
- Swagger/OpenAPI documentation
- CORS-enabled for cross-domain requests
- Docker containerization for deployment
- Comprehensive unit tests
- Azure App Service ready

## Technology Stack

- **Runtime**: .NET 10.0
- **Framework**: ASP.NET Core
- **ORM**: Entity Framework Core 10.0.5
- **Database**: In-Memory (with SQL Server migration support)
- **API Documentation**: Swagger/OpenAPI
- **Container**: Docker with multi-stage builds
- **Deployment**: Azure App Service (Free tier)
- **Testing**: xUnit

## Project Structure

```
oscar-iot/
├── src/
│   └── OscarIoT.Api/
│       └── OscarIoT.Api/
│           ├── Controllers/
│           │   └── SensorReadingsController.cs
│           ├── Models/
│           │   └── SensorReading.cs
│           ├── DTOs/
│           │   └── CreateSensorreadingDTo.cs
│           ├── Data/
│           │   ├── AppDbContext.cs
│           │   └── AppDbContextFactory.cs
│           ├── Migrations/
│           │   └── 20260316133734_InitialCreate.cs
│           ├── Program.cs
│           ├── appsettings.json
│           ├── docker-compose.yml
│           └── dockerfile
├── tests/
│   └── OscarIoT.Tests/
│       └── OscarIoT.Tests/
│           ├── SensorReadingsTests.cs
│           └── UnitTest1.cs
├── .github/
│   └── workflows/
│       └── ci.yml
├── .gitignore
└── README.md
```

## API Endpoints

### Get All Sensor Readings

```http
GET /api/v1/sensorreadings
```

Returns all sensor readings ordered by timestamp (most recent first).

**Response**: HTTP 200 OK
```json
[
  {
    "id": 1,
    "sensorId": "SENSOR-001",
    "sensorType": "temperature",
    "value": 22.5,
    "unit": "celsius",
    "timestamp": "2026-03-20T07:35:00Z",
    "location": "Room A"
  }
]
```

### Get Sensor Reading by ID

```http
GET /api/v1/sensorreadings/{id}
```

Returns a specific sensor reading by its unique identifier.

**Parameters**:
- `id` (integer, required) - The sensor reading ID

**Response**: HTTP 200 OK or 404 Not Found

### Get Readings by Sensor ID

```http
GET /api/v1/sensorreadings/sensor/{sensorId}
```

Returns all readings for a specific sensor.

**Parameters**:
- `sensorId` (string, required) - The sensor's unique identifier

**Response**: HTTP 200 OK

### Create Sensor Reading

```http
POST /api/v1/sensorreadings
```

Creates a new sensor reading.

**Request Body**:
```json
{
  "sensorId": "SENSOR-001",
  "sensorType": "temperature",
  "value": 22.5,
  "unit": "celsius",
  "location": "Room A"
}
```

**Response**: HTTP 201 Created

### Update Sensor Reading

```http
PUT /api/v1/sensorreadings/{id}
```

Updates an existing sensor reading.

**Parameters**:
- `id` (integer, required) - The sensor reading ID

**Request Body**:
```json
{
  "sensorId": "SENSOR-001",
  "sensorType": "temperature",
  "value": 23.1,
  "unit": "celsius",
  "location": "Room A"
}
```

**Response**: HTTP 204 No Content or 404 Not Found

### Delete Sensor Reading

```http
DELETE /api/v1/sensorreadings/{id}
```

Deletes a sensor reading.

**Parameters**:
- `id` (integer, required) - The sensor reading ID

**Response**: HTTP 204 No Content or 404 Not Found

## Installation

### Prerequisites

- .NET 10.0 SDK or later
- Docker (optional, for containerized deployment)
- Git

### Local Development

1. Clone the repository:

```bash
git clone https://github.com/OscarKingIn/IoT-Monitoring-Sensor-Api.git
cd oscar-iot
```

2. Restore dependencies:

```bash
cd src/OscarIoT.Api/OscarIoT.Api
dotnet restore
```

3. Build the project:

```bash
dotnet build --configuration Release
```

4. Run the application:

```bash
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger UI at `https://localhost:5001/swagger/ui`.

### Docker Deployment

1. Build the Docker image:

```bash
docker build -t oscar-iot-api:latest .
```

2. Run the container:

```bash
docker run -p 8080:8080 oscar-iot-api:latest
```

The API will be accessible at `http://localhost:8080`.

## Running Tests

Execute unit tests with:

```bash
cd tests/OscarIoT.Tests/OscarIoT.Tests
dotnet test
```

Test coverage includes:
- Sensor reading retrieval operations
- Data validation
- Database context operations
- HTTP response validation

## Database Configuration

Currently, the application uses an in-memory database for rapid development and testing. To switch to SQL Server:

1. Update `Program.cs`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

2. Add connection string to `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=OscarIoTDb;User Id=sa;Password=YourPassword;"
  }
}
```

3. Apply migrations:

```bash
dotnet ef database update
```

## Deployment

### Azure App Service

The application is configured for Azure App Service deployment:

- **Resource Group**: OscarIoT-rg
- **App Service Name**: oscar-iot-api-2026-01
- **Runtime**: DOTNETCORE | 8.0
- **Region**: South Africa North
- **Pricing Tier**: Free

**Current URL**: https://oscar-iot-api-2026-01.azurewebsites.net

To deploy:

```bash
az login
az webapp deploy --resource-group OscarIoT-rg --name oscar-iot-api-2026-01 --src-path ./publish --type zip
```

## Configuration

### CORS Settings

The API allows all origins (CORS configured for development):

```csharp
options.AddPolicy("AllowAll", policy =>
    policy.SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod()
);
```

For production, modify the policy in `Program.cs` to restrict origins.

### Logging

Configure logging levels in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Project Timeline

- **2026-03-16**: Initial project setup with Entity Framework Core and SQL Server integration
- **2026-03-17**: Added Sensor API with REST endpoints and CORS support
- **2026-03-20**: Dashboard frontend added via static files, CORS configuration refined, cleanup of build artifacts

## Code Standards

This project follows ASP.NET Core best practices:

- Dependency Injection for all services
- RESTful API design conventions
- Data Transfer Objects (DTOs) for API contracts
- Entity Framework Core for data access
- Async/await pattern for I/O operations
- Nullable reference types enabled
- Implicit using directives

## Contributing

1. Create a feature branch:

```bash
git checkout -b feature/your-feature-name
```

2. Commit changes:

```bash
git commit -m "feat: description of changes"
```

3. Push to remote:

```bash
git push origin feature/your-feature-name
```

4. Create a pull request on GitHub

## Security Considerations

- Update CORS policy for production environments
- Implement authentication/authorization mechanisms
- Validate and sanitize all input data
- Use environment variables for sensitive configuration
- Enable HTTPS enforcement
- Implement rate limiting for API endpoints
- Add request logging and monitoring

## Performance Optimization

For production deployments:

1. Switch from in-memory database to SQL Server
2. Implement caching strategies (Redis)
3. Add database indexing on frequently queried columns
4. Configure connection pooling
5. Enable response compression
6. Implement pagination for large datasets
7. Use Azure CDN for static assets

## Troubleshooting

### Port Already in Use

If port 5001/5000 is occupied:

```bash
dotnet run --urls "https://localhost:5003"
```

### Database Context Issues

Clear Entity Framework cache:

```bash
dotnet ef database drop --force
dotnet ef database update
```

### Docker Build Failures

Ensure .NET SDK is up to date:

```bash
dotnet --version
dotnet new global-json --sdk-version 10.0.0 --roll-forward latestFeature
```

## License

This project is proprietary and confidential. Unauthorized access, modification, or distribution is prohibited.

## Support

For issues, feature requests, or questions, contact the development team or open an issue on GitHub.

---

**Last Updated**: March 20, 2026
**Status**: Production Ready
**Maintainer**: Oscar Development Team
