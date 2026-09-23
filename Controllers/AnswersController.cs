using Microsoft.AspNetCore.Mvc;

namespace TestingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllAnswers() => Ok("Список ответов");

    [HttpGet("{id:int}")]
    public IActionResult GetAnswerById(int id)
    {
        if (id <= 0) return BadRequest("Некорректный id");
        if (id == 1) return Ok("Ответ 1");
        return NotFound();
    }

    [HttpPost]
    public IActionResult CreateAnswer()
    {
        return Created("/api/answers/1", "Создан ответ с id=1");
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnswer(int id)
    {
        if (id <= 0) return BadRequest("Некорректный id");
        if (id != 1) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnswer(int id)
    {
        if (id <= 0) return BadRequest("Некорректный id");
        if (id != 1) return NotFound();
        return NoContent();
    }
}