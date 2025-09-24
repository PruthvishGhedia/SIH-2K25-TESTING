using System.Data;

namespace SIH.ERP.Soap.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly IDbConnection _connection;

        protected RepositoryBase(IDbConnection connection)
        {
            _connection = connection;
        }

        protected virtual void EnsureConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }
    }
}