using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IFacultyService
{
    [OperationContract]
    Task<IEnumerable<Faculty>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Faculty?> GetAsync(int faculty_id);
    [OperationContract]
    Task<Faculty> CreateAsync(Faculty item);
    [OperationContract]
    Task<Faculty?> UpdateAsync(int faculty_id, Faculty item);
    [OperationContract]
    Task<Faculty?> RemoveAsync(int faculty_id);
}

