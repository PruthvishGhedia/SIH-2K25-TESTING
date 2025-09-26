using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        
        try
        {
            Console.WriteLine("Testing API endpoints...");
            
            // Test version endpoint
            var versionResponse = await client.GetAsync("http://localhost:5000/api/version");
            Console.WriteLine($"Version endpoint status: {versionResponse.StatusCode}");
            
            if (versionResponse.IsSuccessStatusCode)
            {
                var versionContent = await versionResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Version response: {versionContent}");
            }
            
            // Test documentation endpoint
            var docResponse = await client.GetAsync("http://localhost:5000/api/documentation");
            Console.WriteLine($"Documentation endpoint status: {docResponse.StatusCode}");
            
            if (docResponse.IsSuccessStatusCode)
            {
                var docContent = await docResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Documentation response: {docContent}");
            }
            
            // Test health endpoint
            var healthResponse = await client.GetAsync("http://localhost:5000/api/health");
            Console.WriteLine($"Health endpoint status: {healthResponse.StatusCode}");
            
            if (healthResponse.IsSuccessStatusCode)
            {
                var healthContent = await healthResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Health response: {healthContent}");
            }
            
            // Removed Swagger JSON test
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}