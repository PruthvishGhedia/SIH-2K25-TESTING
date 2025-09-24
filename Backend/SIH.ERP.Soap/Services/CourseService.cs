using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class CourseService : ICourseService
{
    private readonly CourseRepository _repo;
    public CourseService(CourseRepository repo) { _repo = repo; }

    public Task<Course> CreateAsync(Course item) => _repo.CreateAsync(item);
    public Task<Course?> GetAsync(int course_id) => _repo.GetAsync(course_id);
    public Task<IEnumerable<Course>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Course?> RemoveAsync(int course_id) => _repo.RemoveAsync(course_id);
    public Task<Course?> UpdateAsync(int course_id, Course item) => _repo.UpdateAsync(course_id, item);
}

