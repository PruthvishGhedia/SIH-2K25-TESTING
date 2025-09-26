#!/bin/bash

# SIH ERP System - Setup Verification Script
echo "🚀 SIH ERP System - Setup Verification"
echo "======================================"

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to print colored output
print_status() {
    if [ $2 -eq 0 ]; then
        echo -e "${GREEN}✅ $1${NC}"
    else
        echo -e "${RED}❌ $1${NC}"
    fi
}

print_warning() {
    echo -e "${YELLOW}⚠️  $1${NC}"
}

print_info() {
    echo -e "${YELLOW}ℹ️  $1${NC}"
}

# Check if we're in the right directory
if [ ! -f "README.md" ]; then
    echo -e "${RED}❌ Please run this script from the SIH-2k25 root directory${NC}"
    exit 1
fi

echo ""
echo "1. Checking Backend Setup..."
echo "---------------------------"

# Check if .NET 8 is installed
if command -v dotnet &> /dev/null; then
    DOTNET_VERSION=$(dotnet --version)
    if [[ $DOTNET_VERSION == 8.* ]]; then
        print_status "DotNet 8 installed ($DOTNET_VERSION)" 0
    else
        print_warning "DotNet 8 not found (found $DOTNET_VERSION)"
    fi
else
    print_status "DotNet not installed" 1
fi

# Check if PostgreSQL is running (basic check)
if command -v psql &> /dev/null; then
    print_status "PostgreSQL client available" 0
else
    print_warning "PostgreSQL client not found"
fi

# Check backend project structure
if [ -d "Backend/SIH.ERP.Soap" ]; then
    print_status "Backend project structure exists" 0
    
    # Check for key files
    if [ -f "Backend/SIH.ERP.Soap/Program.cs" ]; then
        print_status "Program.cs exists" 0
    else
        print_status "Program.cs missing" 1
    fi
    
    if [ -f "Backend/SIH.ERP.Soap/Middleware/ErrorHandlingMiddleware.cs" ]; then
        print_status "ErrorHandlingMiddleware exists" 0
    else
        print_status "ErrorHandlingMiddleware missing" 1
    fi
    
    if [ -f "Backend/SIH.ERP.Soap/Seed/seed.sql" ]; then
        print_status "Seed data script exists" 0
    else
        print_status "Seed data script missing" 1
    fi
else
    print_status "Backend project structure missing" 1
fi

echo ""
echo "2. Checking Frontend Setup..."
echo "----------------------------"

# Check if Node.js is installed
if command -v node &> /dev/null; then
    NODE_VERSION=$(node --version)
    print_status "Node.js installed ($NODE_VERSION)" 0
else
    print_status "Node.js not installed" 1
fi

# Check if npm is installed
if command -v npm &> /dev/null; then
    NPM_VERSION=$(npm --version)
    print_status "npm installed ($NPM_VERSION)" 0
else
    print_status "npm not installed" 1
fi

# Check frontend project structure
if [ -d "Frontend" ]; then
    print_status "Frontend project structure exists" 0
    
    # Check for key files
    if [ -f "Frontend/package.json" ]; then
        print_status "package.json exists" 0
    else
        print_status "package.json missing" 1
    fi
    
    if [ -f "Frontend/src/services/soapClient.js" ]; then
        print_status "SOAP client exists" 0
    else
        print_status "SOAP client missing" 1
    fi
    
    if [ -f "Frontend/.env.development" ]; then
        print_status "Environment configuration exists" 0
    else
        print_status "Environment configuration missing" 1
    fi
else
    print_status "Frontend project structure missing" 1
fi

echo ""
echo "3. Testing Backend (if running)..."
echo "----------------------------------"

# Test health endpoint
if curl -s http://localhost:5000/health > /dev/null 2>&1; then
    print_status "Backend health endpoint responding" 0
else
    print_warning "Backend not running on localhost:5000"
fi

echo ""
echo "4. Testing Frontend (if running)..."
echo "-----------------------------------"

# Test frontend
if curl -s http://localhost:5173 > /dev/null 2>&1; then
    print_status "Frontend development server responding" 0
else
    print_warning "Frontend not running on localhost:5173"
fi

echo ""
echo "5. Environment Configuration..."
echo "-------------------------------"

# Check for .env files
if [ -f "Backend/SIH.ERP.Soap/.env" ]; then
    print_status "Backend .env file exists" 0
    if grep -q "DATABASE_URL" Backend/SIH.ERP.Soap/.env; then
        print_status "DATABASE_URL configured" 0
    else
        print_warning "DATABASE_URL not configured in backend .env"
    fi
else
    print_warning "Backend .env file missing"
fi

if [ -f "Frontend/.env.development" ]; then
    print_status "Frontend .env.development exists" 0
    if grep -q "VITE_BACKEND_BASE" Frontend/.env.development; then
        print_status "VITE_BACKEND_BASE configured" 0
    else
        print_warning "VITE_BACKEND_BASE not configured"
    fi
else
    print_warning "Frontend .env.development missing"
fi

echo ""
echo "6. Next Steps..."
echo "---------------"

print_info "To start the backend:"
echo "  cd Backend/SIH.ERP.Soap"
echo "  dotnet restore"
echo "  dotnet run"

print_info "To start the frontend:"
echo "  cd Frontend"
echo "  npm install"
echo "  npm run dev"

print_info "To seed the database:"
echo "  psql -d your_database -f Backend/SIH.ERP.Soap/Seed/seed.sql"

echo ""
echo "🎉 Verification complete!"
echo "Check the status above and follow the next steps to get started."