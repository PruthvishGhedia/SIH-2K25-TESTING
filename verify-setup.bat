@echo off
REM SIH ERP System - Setup Verification Script (Windows)
echo 🚀 SIH ERP System - Setup Verification
echo ======================================
echo.

echo 1. Checking Backend Setup...
echo ---------------------------

REM Check if .NET 8 is installed
dotnet --version >nul 2>&1
if %errorlevel% == 0 (
    echo ✅ DotNet installed
    dotnet --version
) else (
    echo ❌ DotNet not installed
)

REM Check if PostgreSQL is available
psql --version >nul 2>&1
if %errorlevel% == 0 (
    echo ✅ PostgreSQL client available
) else (
    echo ⚠️  PostgreSQL client not found
)

REM Check backend project structure
if exist "Backend\SIH.ERP.Soap" (
    echo ✅ Backend project structure exists
    
    if exist "Backend\SIH.ERP.Soap\Program.cs" (
        echo ✅ Program.cs exists
    ) else (
        echo ❌ Program.cs missing
    )
    
    if exist "Backend\SIH.ERP.Soap\Middleware\ErrorHandlingMiddleware.cs" (
        echo ✅ ErrorHandlingMiddleware exists
    ) else (
        echo ❌ ErrorHandlingMiddleware missing
    )
    
    if exist "Backend\SIH.ERP.Soap\Seed\seed.sql" (
        echo ✅ Seed data script exists
    ) else (
        echo ❌ Seed data script missing
    )
) else (
    echo ❌ Backend project structure missing
)

echo.
echo 2. Checking Frontend Setup...
echo ----------------------------

REM Check if Node.js is installed
node --version >nul 2>&1
if %errorlevel% == 0 (
    echo ✅ Node.js installed
    node --version
) else (
    echo ❌ Node.js not installed
)

REM Check if npm is installed
npm --version >nul 2>&1
if %errorlevel% == 0 (
    echo ✅ npm installed
    npm --version
) else (
    echo ❌ npm not installed
)

REM Check frontend project structure
if exist "Frontend" (
    echo ✅ Frontend project structure exists
    
    if exist "Frontend\package.json" (
        echo ✅ package.json exists
    ) else (
        echo ❌ package.json missing
    )
    
    if exist "Frontend\src\services\soapClient.js" (
        echo ✅ SOAP client exists
    ) else (
        echo ❌ SOAP client missing
    )
    
    if exist "Frontend\.env.development" (
        echo ✅ Environment configuration exists
    ) else (
        echo ❌ Environment configuration missing
    )
) else (
    echo ❌ Frontend project structure missing
)

echo.
echo 3. Testing Backend (if running)...
echo ----------------------------------

REM Test health endpoint
curl -s http://localhost:5000/health >nul 2>&1
if %errorlevel% == 0 (
    echo ✅ Backend health endpoint responding
) else (
    echo ⚠️  Backend not running on localhost:5000
)

echo.
echo 4. Testing Frontend (if running)...
echo -----------------------------------

REM Test frontend
curl -s http://localhost:5173 >nul 2>&1
if %errorlevel% == 0 (
    echo ✅ Frontend development server responding
) else (
    echo ⚠️  Frontend not running on localhost:5173
)

echo.
echo 5. Environment Configuration...
echo -------------------------------

REM Check for .env files
if exist "Backend\SIH.ERP.Soap\.env" (
    echo ✅ Backend .env file exists
) else (
    echo ⚠️  Backend .env file missing
)

if exist "Frontend\.env.development" (
    echo ✅ Frontend .env.development exists
) else (
    echo ⚠️  Frontend .env.development missing
)

echo.
echo 6. Next Steps...
echo ---------------

echo ℹ️  To start the backend:
echo   cd Backend\SIH.ERP.Soap
echo   dotnet restore
echo   dotnet run

echo.
echo ℹ️  To start the frontend:
echo   cd Frontend
echo   npm install
echo   npm run dev

echo.
echo ℹ️  To seed the database:
echo   psql -d your_database -f Backend\SIH.ERP.Soap\Seed\seed.sql

echo.
echo ℹ️  To run tests:
echo   cd Backend\SIH.ERP.Soap ^&^& dotnet test

echo.
echo 🎉 Verification complete!
echo Check the status above and follow the next steps to get started.
pause