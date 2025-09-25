using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SIH.ERP.Soap.Health
{
    public class CustomPostgreSqlHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public CustomPostgreSqlHealthCheck(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync(cancellationToken);
                
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT 1";
                await command.ExecuteScalarAsync(cancellationToken);
                
                return HealthCheckResult.Healthy("PostgreSQL connection is healthy");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("PostgreSQL connection is unhealthy", ex);
            }
        }
    }
}