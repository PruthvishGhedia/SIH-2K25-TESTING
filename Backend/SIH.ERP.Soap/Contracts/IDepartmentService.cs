using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IDepartmentService
{
    [OperationContract]
    Task<IEnumerable<Department>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Department?> GetAsync(int dept_id);

    [OperationContract]
    Task<Department> CreateAsync(Department item);

    [OperationContract]
    Task<Department?> UpdateAsync(int dept_id, Department item);

    [OperationContract]
    Task<Department?> RemoveAsync(int dept_id);
}

