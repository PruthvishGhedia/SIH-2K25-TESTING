using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repo;
    public PaymentService(IPaymentRepository repo) { _repo = repo; }

    public Task<IEnumerable<Payment>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Payment?> GetAsync(int payment_id) => _repo.GetAsync(payment_id);

    public async Task<Payment> CreateAsync(Payment item)
    {
        Validate(item);
        return await _repo.CreateAsync(item);
    }

    public async Task<Payment?> UpdateAsync(int payment_id, Payment item)
    {
        Validate(item);
        return await _repo.UpdateAsync(payment_id, item);
    }

    public Task<Payment?> RemoveAsync(int payment_id) => _repo.RemoveAsync(payment_id);

    private void Validate(Payment p)
    {
        if (p.student_id <= 0) throw new FaultException("student_id is required");
        if (p.amount <= 0) throw new FaultException("amount must be > 0");
        if (string.IsNullOrWhiteSpace(p.payment_date)) throw new FaultException("payment_date is required");
    }
}

