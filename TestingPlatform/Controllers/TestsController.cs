using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Requests.Test;
using TestingPlatform.Responses.Test;

namespace TestingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController(ITestRepository testRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTests(
        [FromQuery] bool? isPublic,
        [FromQuery] List<int> groupIds,
        [FromQuery] List<int> studentIds)
    {
        var tests = await testRepository.GetAllAsync(isPublic, groupIds, studentIds);
        return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
    }

    [HttpGet("student/{studentId:int}")]
    public async Task<IActionResult> GetTestsForStudent(int studentId)
    {
        var tests = await testRepository.GetAllForStudent(studentId);
        return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetTopRecent([FromQuery] int count = 5)
    {
        var tests = await testRepository.GetTopRecentAsync(count);
        return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTestById(int id)
    {
        var test = await testRepository.GetByIdAsync(id);
        return Ok(mapper.Map<TestResponse>(test));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] CreateTestRequest test)
    {
        var id = await testRepository.CreateAsync(mapper.Map<TestDto>(test));
        return StatusCode(StatusCodes.Status201Created, new { Id = id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTest(int id, [FromBody] UpdateTestRequest test)
    {
        test.Id = id;
        await testRepository.UpdateAsync(mapper.Map<TestDto>(test));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTest(int id)
    {
        await testRepository.DeleteAsync(id);
        return NoContent();
    }
}