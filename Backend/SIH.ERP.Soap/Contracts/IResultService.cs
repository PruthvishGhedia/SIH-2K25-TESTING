using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IResultService
{
    [OperationContract]
    Task<IEnumerable<Result>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Result?> GetAsync(int result_id);

    [OperationContract]
    Task<Result> CreateAsync(Result item);

    [OperationContract]
    Task<Result?> UpdateAsync(int result_id, Result item);

    [OperationContract]
    Task<Result?> RemoveAsync(int result_id);
}