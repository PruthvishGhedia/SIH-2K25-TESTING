using System.Data;

namespace SIH.ERP.Soap.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new Npgsql.NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}