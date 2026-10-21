using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Repositories;

public class AnswerRepository(AppDbContext appDbContext, IMapper mapper) : IAnswerRepository
{
    public async Task<IEnumerable<AnswerDto>> GetAllAsync()
    {
        var answers = await appDbContext.Answers.AsNoTracking().ToListAsync();
        return mapper.Map<IEnumerable<AnswerDto>>(answers);
    }

    public async Task<AnswerDto> GetByIdAsync(int id)
    {
        var answer = await appDbContext.Answers.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if (answer == null) throw new Exception("Ответ не найден.");
        return mapper.Map<AnswerDto>(answer);
    }

    public async Task<int> CreateAsync(AnswerDto answerDto)
    {
        var answer = mapper.Map<Answer>(answerDto);
        await appDbContext.AddAsync(answer);
        await appDbContext.SaveChangesAsync();
        return answer.Id;
    }

    public async Task UpdateAsync(AnswerDto answerDto)
    {
        var answer = await appDbContext.Answers.FirstOrDefaultAsync(a => a.Id == answerDto.Id);
        if (answer == null) throw new Exception("Ответ не найден.");

        answer.Text = answerDto.Text;
        answer.IsCorrect = answerDto.IsCorrect;
        answer.QuestionId = answerDto.QuestionId;

        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var answer = await appDbContext.Answers.FirstOrDefaultAsync(a => a.Id == id);
        if (answer == null) throw new Exception("Ответ не найден.");

        appDbContext.Answers.Remove(answer);
        await appDbContext.SaveChangesAsync();
    }
}