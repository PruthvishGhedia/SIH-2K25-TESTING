using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class ExamService : IExamService
{
    private readonly ExamRepository _repo;
    public ExamService(ExamRepository repo)
    {
        _repo = repo;
    }

    public Task<Exam> CreateAsync(Exam item) => _repo.CreateAsync(item);
    public Task<Exam?> GetAsync(int exam_id) => _repo.GetAsync(exam_id);
    public Task<IEnumerable<Exam>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Exam?> RemoveAsync(int exam_id) => _repo.RemoveAsync(exam_id);
    public Task<Exam?> UpdateAsync(int exam_id, Exam item) => _repo.UpdateAsync(exam_id, item);
}