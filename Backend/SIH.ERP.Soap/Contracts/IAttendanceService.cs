using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IAttendanceService
{
    [OperationContract]
    Task<IEnumerable<Attendance>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Attendance?> GetAsync(int attendance_id);
    [OperationContract]
    Task<Attendance> CreateAsync(Attendance item);
    [OperationContract]
    Task<Attendance?> UpdateAsync(int attendance_id, Attendance item);
    [OperationContract]
    Task<Attendance?> RemoveAsync(int attendance_id);
}

