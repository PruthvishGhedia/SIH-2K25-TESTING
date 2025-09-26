using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IPaymentRepository : IRepository
{
    Task<IEnumerable<Payment>> ListAsync(int limit, int offset);
    Task<Payment?> GetAsync(int id);
    Task<Payment> CreateAsync(Payment item);
    Task<Payment?> UpdateAsync(int id, Payment item);
    Task<Payment?> RemoveAsync(int id);
}