using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IUserRoleService
{
    [OperationContract]
    Task<IEnumerable<UserRole>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<UserRole?> GetAsync(int user_role_id);

    [OperationContract]
    Task<UserRole> CreateAsync(UserRole item);

    [OperationContract]
    Task<UserRole?> UpdateAsync(int user_role_id, UserRole item);

    [OperationContract]
    Task<UserRole?> RemoveAsync(int user_role_id);
}