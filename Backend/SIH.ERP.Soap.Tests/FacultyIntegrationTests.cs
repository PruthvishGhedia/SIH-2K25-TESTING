using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using Xunit;

namespace SIH.ERP.Soap.Tests
{
    public class FacultyIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public FacultyIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task List_ShouldReturnSoapEnvelope()
        {
            var soapRequest = @"<?xml version=\"1.0\" encoding=\"utf-8\"?>
<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">
  <soap:Body>
    <ListAsync xmlns=\"http://tempuri.org/\">
      <limit>5</limit>
      <offset>0</offset>
    </ListAsync>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            _client.DefaultRequestHeaders.Remove("SOAPAction");
            _client.DefaultRequestHeaders.Add("SOAPAction", "\"http://tempuri.org/IFacultyService/ListAsync\"");
            var response = await _client.PostAsync("/soap/faculty", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("soap:Envelope", responseContent);
        }
    }
}

