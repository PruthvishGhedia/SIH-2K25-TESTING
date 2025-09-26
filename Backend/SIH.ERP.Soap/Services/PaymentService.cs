using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public PaymentService(IPaymentRepository repo, IHubContext<DashboardHub> hubContext) 
    { 
        _repo = repo; 
        _hubContext = hubContext;
    }

    public Task<IEnumerable<Payment>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Payment?> GetAsync(string payment_id)
    {
        if (int.TryParse(payment_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }

    public async Task<Payment> CreateAsync(Payment item)
    {
        Validate(item);
        var payment = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceivePaymentUpdate", payment);
        
        return payment;
    }

    public async Task<Payment?> UpdateAsync(string payment_id, Payment item)
    {
        if (int.TryParse(payment_id, out int id))
        {
            Validate(item);
            var payment = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (payment != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceivePaymentUpdate", payment);
            }
            
            return payment;
        }
        return null;
    }

    public async Task<Payment?> RemoveAsync(string payment_id)
    {
        if (int.TryParse(payment_id, out int id))
        {
            var payment = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (payment != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceivePaymentUpdate", payment);
            }
            
            return payment;
        }
        return null;
    }

    private void Validate(Payment p)
    {
        if (p.student_id <= 0) throw new FaultException("student_id is required");
        if (p.amount <= 0) throw new FaultException("amount must be > 0");
        if (string.IsNullOrWhiteSpace(p.payment_date)) throw new FaultException("payment_date is required");
    }
}