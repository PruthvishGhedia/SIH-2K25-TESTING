using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface ISubjectService
{
    [OperationContract]
    Task<IEnumerable<Subject>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Subject?> GetAsync(int subject_code);
    [OperationContract]
    Task<Subject> CreateAsync(Subject item);
    [OperationContract]
    Task<Subject?> UpdateAsync(int subject_code, Subject item);
    [OperationContract]
    Task<Subject?> RemoveAsync(int subject_code);
}

