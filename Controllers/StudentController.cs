using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Data;
using TestingPlatform.Models;

namespace TestingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public StudentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = _db.Students.Include(s => s.User).ToList();
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetStudentById(int id)
    {
        if (id <= 0)
            return BadRequest("Некорректный id");

        var student = _db.Students.Include(s => s.User).FirstOrDefault(s => s.Id == id);
        if (student is null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        var emailExists = _db.Students.Where(s => s.User.Email == student.User.Email).ToList();
        if (emailExists.Any())
            return Conflict("Такой email уже используется");

        _db.Students.Add(student);
        _db.SaveChanges();

        return Created();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateStudent([FromBody] Student student)
    {
        var exists = _db.Students.FirstOrDefault(s => s.Id == student.Id);
        if (exists == default)
            return NotFound();

        var emailInUse = _db.Students.Any(s => s.User.Email == student.User.Email && s.Id != student.Id);
        if (emailInUse)
            return Conflict("Такой email уже используется");

        _db.Entry(student).State = EntityState.Modified;
        _db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = _db.Students.Find(id);
        if (student is null)
            return NotFound();

        _db.Students.Remove(student);
        _db.SaveChanges();

        return NoContent();
    }
}