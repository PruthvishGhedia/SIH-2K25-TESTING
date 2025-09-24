using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IRoomService
{
    [OperationContract]
    Task<IEnumerable<Room>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Room?> GetAsync(int room_id);

    [OperationContract]
    Task<Room> CreateAsync(Room item);

    [OperationContract]
    Task<Room?> UpdateAsync(int room_id, Room item);

    [OperationContract]
    Task<Room?> RemoveAsync(int room_id);
}