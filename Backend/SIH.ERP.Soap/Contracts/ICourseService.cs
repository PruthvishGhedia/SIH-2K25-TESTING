using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface ICourseService
{
    [OperationContract]
    Task<IEnumerable<Course>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Course?> GetAsync(int course_id);
    [OperationContract]
    Task<Course> CreateAsync(Course item);
    [OperationContract]
    Task<Course?> UpdateAsync(int course_id, Course item);
    [OperationContract]
    Task<Course?> RemoveAsync(int course_id);
}

