using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class ExamService : IExamService
{
    private readonly ExamRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public ExamService(ExamRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public async Task<Exam> CreateAsync(Exam item)
    {
        var exam = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveExamUpdate", exam);
        
        return exam;
    }
    
    public async Task<Exam?> GetAsync(string exam_id)
    {
        if (int.TryParse(exam_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Exam>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Exam?> RemoveAsync(string exam_id)
    {
        if (int.TryParse(exam_id, out int id))
        {
            var exam = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (exam != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveExamUpdate", exam);
            }
            
            return exam;
        }
        return null;
    }
    
    public async Task<Exam?> UpdateAsync(string exam_id, Exam item)
    {
        if (int.TryParse(exam_id, out int id))
        {
            var exam = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (exam != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveExamUpdate", exam);
            }
            
            return exam;
        }
        return null;
    }
}