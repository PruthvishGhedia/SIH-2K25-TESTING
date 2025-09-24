using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IRoleService
{
    [OperationContract]
    Task<IEnumerable<Role>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Role?> GetAsync(int role_id);
    [OperationContract]
    Task<Role> CreateAsync(Role item);
    [OperationContract]
    Task<Role?> UpdateAsync(int role_id, Role item);
    [OperationContract]
    Task<Role?> RemoveAsync(int role_id);
}

