# Test script for SIH ERP SOAP API endpoints
# This script tests the basic functionality of the SOAP API

# Test the health endpoint
Write-Host "Testing health endpoint..."
Invoke-WebRequest -Uri "http://localhost:5000/swagger" -Method GET
Write-Host "Health endpoint test completed."

# Test the SOAP student list endpoint
Write-Host "Testing SOAP student list endpoint..."

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

# Headers
$headers = @{
    "Content-Type" = "text/xml"
    "SOAPAction" = '"http://tempuri.org/IStudentService/ListAsync"'
}

try {
    $response = Invoke-WebRequest -Uri "http://localhost:5000/soap/student" -Method POST -Body $soapRequestBody -Headers $headers
    Write-Host "SOAP student list endpoint test completed. Status: $($response.StatusCode)"
    Write-Host "Response: $($response.Content.Substring(0, [Math]::Min(200, $response.Content.Length)))..."
} catch {
    Write-Host "Error testing SOAP student list endpoint: $($_.Exception.Message)"
}

Write-Host "Test script completed."