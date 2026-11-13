using lab12_api.Data;
using lab12_api.Dtos;
using lab12_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab12_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Instructor>>> GetAll() =>
        await db.Instructores.AsNoTracking().Select(i => new Instructor
        {
            IdInstructor = i.IdInstructor, Nombres = i.Nombres, Apellidos = i.Apellidos,
            Especialidad = i.Especialidad, Email = i.Email, Activo = i.Activo
        }).ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Instructor>> GetById(int id)
    {
        var item = await db.Instructores.AsNoTracking().FirstOrDefaultAsync(i => i.IdInstructor == id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Instructor>> Create(InstructorDto dto)
    {
        var item = new Instructor
        {
            Nombres = dto.Nombres, Apellidos = dto.Apellidos, Especialidad = dto.Especialidad,
            Email = dto.Email, Activo = dto.Activo
        };
        db.Instructores.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.IdInstructor }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, InstructorDto dto)
    {
        var item = await db.Instructores.FindAsync(id);
        if (item is null) return NotFound();
        item.Nombres = dto.Nombres; item.Apellidos = dto.Apellidos; item.Especialidad = dto.Especialidad;
        item.Email = dto.Email; item.Activo = dto.Activo;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Instructores.FindAsync(id);
        if (item is null) return NotFound();
        if (await db.Cursos.AnyAsync(c => c.IdInstructor == id))
            return Conflict("No se puede eliminar: el instructor tiene cursos asignados.");
        db.Instructores.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
