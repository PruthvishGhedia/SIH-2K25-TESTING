using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using Xunit;

namespace SIH.ERP.Soap.Tests
{
    public class StudentIntegrationTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public StudentIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task HealthCheck_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/health");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Healthy", content);
        }

        [Fact]
        public async Task SoapStudentList_ShouldReturnSoapEnvelope()
        {
            // Arrange
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <ListAsync xmlns=""http://tempuri.org/"">
      <limit>10</limit>
      <offset>0</offset>
    </ListAsync>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            _client.DefaultRequestHeaders.Add("SOAPAction", @"""http://tempuri.org/IStudentService/ListAsync""");

            // Act
            var response = await _client.PostAsync("/soap/student", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("soap:Envelope", responseContent);
            Assert.Contains("soap:Body", responseContent);
        }

        [Fact]
        public async Task SoapStudentGet_ShouldReturnSoapEnvelope()
        {
            // Arrange
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <GetAsync xmlns=""http://tempuri.org/"">
      <student_id>1</student_id>
    </GetAsync>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            _client.DefaultRequestHeaders.Add("SOAPAction", @"""http://tempuri.org/IStudentService/GetAsync""");

            // Act
            var response = await _client.PostAsync("/soap/student", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("soap:Envelope", responseContent);
            Assert.Contains("soap:Body", responseContent);
        }

        [Fact]
        public async Task SoapStudentCreate_ShouldReturnSoapEnvelope()
        {
            // Arrange
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <CreateAsync xmlns=""http://tempuri.org/"">
      <item>
        <first_name>Integration</first_name>
        <last_name>Test</last_name>
        <dob>2000-01-01T00:00:00</dob>
        <email>integration@test.com</email>
        <department_id>1</department_id>
        <course_id>1</course_id>
        <verified>true</verified>
      </item>
    </CreateAsync>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            _client.DefaultRequestHeaders.Add("SOAPAction", @"""http://tempuri.org/IStudentService/CreateAsync""");

            // Act
            var response = await _client.PostAsync("/soap/student", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("soap:Envelope", responseContent);
            Assert.Contains("soap:Body", responseContent);
        }

        [Fact]
        public async Task SoapStudentCreate_WithInvalidData_ShouldReturnSoapFault()
        {
            // Arrange
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <CreateAsync xmlns=""http://tempuri.org/"">
      <item>
        <first_name></first_name>
        <last_name></last_name>
        <email></email>
      </item>
    </CreateAsync>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            _client.DefaultRequestHeaders.Add("SOAPAction", @"""http://tempuri.org/IStudentService/CreateAsync""");

            // Act
            var response = await _client.PostAsync("/soap/student", content);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("soap:Fault", responseContent);
            Assert.Contains("faultcode", responseContent);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}