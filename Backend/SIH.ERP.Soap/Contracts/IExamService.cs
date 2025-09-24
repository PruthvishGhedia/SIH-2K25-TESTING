using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IExamService
{
    [OperationContract]
    Task<IEnumerable<Exam>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Exam?> GetAsync(int exam_id);

    [OperationContract]
    Task<Exam> CreateAsync(Exam item);

    [OperationContract]
    Task<Exam?> UpdateAsync(int exam_id, Exam item);

    [OperationContract]
    Task<Exam?> RemoveAsync(int exam_id);
}