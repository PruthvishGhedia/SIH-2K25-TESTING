# SIH ERP SOAP API - Windows Setup Guide

## Prerequisites
- .NET 8 SDK
- PostgreSQL database
- PowerShell 5.1 or later

## Setup Instructions

### 1. Restore Packages and Build Project

```powershell
# Navigate to the backend project directory
cd Backend\SIH.ERP.Soap

# Restore NuGet packages
dotnet restore

# Build the project
dotnet build
```

### 2. Configure Environment

Create a `.env` file in the `Backend\SIH.ERP.Soap` directory with your PostgreSQL connection string:

```env
DATABASE_URL=Host=localhost;Database=sih_erp;Username=postgres;Password=yourpassword
```

Alternatively, you can modify the `appsettings.json` file to set your connection string.

### 3. Run the Backend Server

```powershell
# From the Backend\SIH.ERP.Soap directory
dotnet run
```

The server will start on `http://localhost:5000` by default.

### 4. Test Health Endpoint

Using PowerShell curl (Invoke-WebRequest):

```powershell
# Test health endpoint
Invoke-WebRequest -Uri "http://localhost:5000/health" -Method GET
```

Or using curl command (if available):

```bash
curl http://localhost:5000/health
```

### 5. Test SOAP Endpoints

Test the Student SOAP service using PowerShell:

```powershell
# SOAP request body
$soapRequestBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <ListAsync xmlns="http://tempuri.org/">
      <limit>10</limit>
      <offset>0</offset>
    </ListAsync>
  </soap:Body>
</soap:Envelope>
"@

# Send SOAP request
Invoke-WebRequest -Uri "http://localhost:5000/soap/student" -Method POST -Body $soapRequestBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction"="`"http://tempuri.org/IStudentService/ListAsync`""}
```

Or using curl command (if available):

```bash
curl -X POST http://localhost:5000/soap/student \
  -H "Content-Type: text/xml; charset=utf-8" \
  -H "SOAPAction: \"http://tempuri.org/IStudentService/ListAsync\"" \
  -d "<?xml version=\"1.0\" encoding=\"utf-8\"?>
<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">
  <soap:Body>
    <ListAsync xmlns=\"http://tempuri.org/\">
      <limit>10</limit>
      <offset>0</offset>
    </ListAsync>
  </soap:Body>
</soap:Envelope>"
```

## Available SOAP Endpoints

- `/soap/student` - Student management
- `/soap/course` - Course management
- `/soap/department` - Department management
- `/soap/user` - User management
- `/soap/fees` - Fees management
- `/soap/exam` - Exam management
- `/soap/guardian` - Guardian management
- `/soap/admission` - Admission management
- `/soap/hostel` - Hostel management
- `/soap/library` - Library management
- `/soap/faculty` - Faculty management
- `/soap/enrollment` - Enrollment management
- `/soap/attendance` - Attendance management
- `/soap/payment` - Payment management

All endpoints support CRUD operations through their respective service contracts.