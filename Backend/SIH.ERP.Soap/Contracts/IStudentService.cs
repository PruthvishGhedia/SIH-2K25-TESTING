using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IStudentService
{
    [OperationContract]
    Task<IEnumerable<Student>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Student?> GetAsync(int student_id);
    [OperationContract]
    Task<Student> CreateAsync(Student item);
    [OperationContract]
    Task<Student?> UpdateAsync(int student_id, Student item);
    [OperationContract]
    Task<Student?> RemoveAsync(int student_id);
}

