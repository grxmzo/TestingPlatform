using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Repositories;

public class TestRepository(AppDbContext appDbContext, IMapper mapper) : ITestRepository
{
    public async Task<IEnumerable<TestDto>> GetAllAsync(bool? isPublic, List<int> groupIds, List<int> studentIds)
    {
        var tests = appDbContext.Tests
            .OrderByDescending(t => t.PublishedAt)
            .ThenBy(t => t.Title)
            .AsNoTracking()
            .AsQueryable();

        if (isPublic is not null)
            tests = tests.Where(t => t.IsPublic == isPublic);

        if (groupIds != null && groupIds.Any())
            tests = tests.Where(t => t.Groups.Any(g => groupIds.Contains(g.Id)));

        if (studentIds != null && studentIds.Any())
            tests = tests.Where(t => t.Students.Any(s => studentIds.Contains(s.Id)));

        return mapper.Map<IEnumerable<TestDto>>(await tests.ToListAsync());
    }

    public async Task<IEnumerable<TestDto>> GetAllForStudent(int studentId)
    {
        var tests = await appDbContext.Tests
            .Where(t => t.IsPublic)
            .Where(t =>
                t.Students.Any(s => s.Id == studentId)
                || t.Courses.Any(c => c.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
                || t.Projects.Any(p => p.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
                || t.Directions.Any(d => d.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
            )
            .ToListAsync();

        return mapper.Map<IEnumerable<TestDto>>(tests);
    }

    public async Task<TestDto> GetByIdAsync(int id)
    {
        var test = await appDbContext.Tests
            .Include(test => test.Directions)
            .Include(test => test.Courses)
            .Include(test => test.Groups)
            .Include(test => test.Projects)
            .Include(test => test.Students)
            .ThenInclude(student => student.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(test => test.Id == id);

        if (test == null) throw new Exception("Тест не найден.");

        return mapper.Map<TestDto>(test);
    }

    public async Task<int> CreateAsync(TestDto testDto)
    {
        var test = mapper.Map<Test>(testDto);
        var testId = await appDbContext.AddAsync(test);
        await appDbContext.SaveChangesAsync();
        return testId.Entity.Id;
    }

    public async Task UpdateAsync(TestDto testDto)
    {
        var test = await appDbContext.Tests.FirstOrDefaultAsync(test => test.Id == testDto.Id);
        if (test == null) throw new Exception("Тест не найден.");

        test.Title = testDto.Title;
        test.Description = testDto.Description;
        test.IsRepeatable = testDto.IsRepeatable;
        test.Type = testDto.Type;
        test.PublishedAt = testDto.PublishedAt;
        test.Deadline = testDto.Deadline;
        test.DurationMinutes = testDto.DurationMinutes;
        test.IsPublic = testDto.IsPublic;
        test.PassingScore = testDto.PassingScore;
        test.MaxAttempts = testDto.MaxAttempts;

        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var test = await appDbContext.Tests.FirstOrDefaultAsync(test => test.Id == id);
        if (test == null) throw new Exception("Тест не найден.");

        appDbContext.Tests.Remove(test);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TestDto>> GetTopRecentAsync(int count = 5)
    {
        var tests = await appDbContext.Tests.AsNoTracking()
            .OrderByDescending(t => t.PublishedAt)
            .ThenByDescending(t => t.Id)
            .Take(count)
            .ToListAsync();

        return mapper.Map<IEnumerable<TestDto>>(tests);
    }
}