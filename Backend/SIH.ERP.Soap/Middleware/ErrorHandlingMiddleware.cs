using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using System.Text.Json;

namespace SIH.ERP.Soap.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.Error.WriteLine($"Error: {ex}");
                
                // Set response content type based on endpoint
                bool isSoapEndpoint = context.Request.Path.StartsWithSegments("/soap");
                context.Response.ContentType = isSoapEndpoint ? "application/xml" : "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (isSoapEndpoint)
                {
                    // Return SOAP Fault envelope
                    var fault = new XDocument(
                        new XElement("soap:Envelope",
                            new XAttribute(XNamespace.Xmlns + "soap", "http://schemas.xmlsoap.org/soap/envelope/"),
                            new XElement("soap:Body",
                                new XElement("soap:Fault",
                                    new XElement("faultcode", "Server"),
                                    new XElement("faultstring", ex.Message)
                                )
                            )
                        )
                    );
                    await context.Response.WriteAsync(fault.ToString());
                }
                else
                {
                    // Return JSON error response
                    var payload = JsonSerializer.Serialize(new { 
                        error = "Internal Server Error", 
                        message = ex.Message,
                        timestamp = DateTime.UtcNow
                    });
                    await context.Response.WriteAsync(payload);
                }
            }
        }
    }
}