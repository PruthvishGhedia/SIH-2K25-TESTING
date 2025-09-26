# Database Setup Instructions

## Prerequisites

1. PostgreSQL database server installed and running
2. A PostgreSQL client (psql, pgAdmin, etc.)

## Database Setup

1. Create a new database:
   ```sql
   CREATE DATABASE sih_erp;
   ```

2. Connect to the database and create a user:
   ```sql
   CREATE USER sih_user WITH PASSWORD 'sih_password';
   GRANT ALL PRIVILEGES ON DATABASE sih_erp TO sih_user;
   ```

3. Run the seed script to create tables and insert sample data:
   ```bash
   psql -U sih_user -d sih_erp -f Seed/seed.sql
   ```

## Environment Configuration

Create a `.env` file in the project root with the following content:
```
DATABASE_URL=Host=localhost;Database=sih_erp;Username=sih_user;Password=sih_password
```

## Alternative: Using appsettings.json

You can also configure the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "SIH": "Host=localhost;Database=sih_erp;Username=sih_user;Password=sih_password"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Testing the Connection

After setting up the database, you can test the connection by running the application:
```bash
dotnet run
```

Then navigate to `http://localhost:5000/swagger` to see the API documentation.