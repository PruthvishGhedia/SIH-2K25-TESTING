using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class CourseService : ICourseService
{
    private readonly CourseRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public CourseService(CourseRepository repo, IHubContext<DashboardHub> hubContext) 
    { 
        _repo = repo; 
        _hubContext = hubContext;
    }

    public async Task<Course> CreateAsync(Course item)
    {
        var course = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveCourseUpdate", course);
        
        return course;
    }
    
    public async Task<Course?> GetAsync(string course_id)
    {
        if (int.TryParse(course_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Course>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Course?> RemoveAsync(string course_id)
    {
        if (int.TryParse(course_id, out int id))
        {
            var course = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (course != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveCourseUpdate", course);
            }
            
            return course;
        }
        return null;
    }
    
    public async Task<Course?> UpdateAsync(string course_id, Course item)
    {
        if (int.TryParse(course_id, out int id))
        {
            var course = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (course != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveCourseUpdate", course);
            }
            
            return course;
        }
        return null;
    }
}