using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IUserService
{
    [OperationContract]
    Task<IEnumerable<User>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<User?> GetAsync(int user_id);

    [OperationContract]
    Task<User> CreateAsync(User item);

    [OperationContract]
    Task<User?> UpdateAsync(int user_id, User item);

    [OperationContract]
    Task<User?> RemoveAsync(int user_id);
}