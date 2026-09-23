using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Data;
using TestingPlatform.Models;

namespace TestingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly AppDbContext _db;

    public GroupsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAllGroups()
    {
        var groups = _db.Groups
            .Include(g => g.Project)
            .Include(g => g.Course)
            .Include(g => g.Direction)
            .Include(g => g.Students)
            .ToList();
        return Ok(groups);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetGroupById(int id)
    {
        if (id <= 0)
            return BadRequest("Некорректный id");

        var group = _db.Groups
            .Include(g => g.Project)
            .Include(g => g.Course)
            .Include(g => g.Direction)
            .Include(g => g.Students)
            .FirstOrDefault(g => g.Id == id);

        if (group is null)
            return NotFound();

        return Ok(group);
    }

    [HttpPost]
    public IActionResult CreateGroup([FromBody] Group group)
    {
        _db.Groups.Add(group);
        _db.SaveChanges();

        return Created();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateGroup([FromBody] Group group)
    {
        var exists = _db.Groups.FirstOrDefault(g => g.Id == group.Id);
        if (exists == default)
            return NotFound();

        _db.Entry(group).State = EntityState.Modified;
        _db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteGroup(int id)
    {
        var group = _db.Groups.Find(id);
        if (group is null)
            return NotFound();

        _db.Groups.Remove(group);
        _db.SaveChanges();

        return NoContent();
    }
}