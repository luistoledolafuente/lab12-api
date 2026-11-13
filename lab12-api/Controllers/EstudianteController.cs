using lab12_api.Data;
using lab12_api.Dtos;
using lab12_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab12_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudianteController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudiante>>> GetAll() =>
        await db.Estudiantes.AsNoTracking().Select(e => new Estudiante
        {
            IdEstudiante = e.IdEstudiante, Nombres = e.Nombres, Apellidos = e.Apellidos,
            Email = e.Email, Telefono = e.Telefono, FechaRegistro = e.FechaRegistro, Activo = e.Activo
        }).ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Estudiante>> GetById(int id)
    {
        var item = await db.Estudiantes.AsNoTracking().FirstOrDefaultAsync(e => e.IdEstudiante == id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Estudiante>> Create(EstudianteDto dto)
    {
        var item = new Estudiante
        {
            Nombres = dto.Nombres, Apellidos = dto.Apellidos, Email = dto.Email,
            Telefono = dto.Telefono, Activo = dto.Activo, FechaRegistro = DateTime.Now
        };
        db.Estudiantes.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.IdEstudiante }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EstudianteDto dto)
    {
        var item = await db.Estudiantes.FindAsync(id);
        if (item is null) return NotFound();
        item.Nombres = dto.Nombres; item.Apellidos = dto.Apellidos; item.Email = dto.Email;
        item.Telefono = dto.Telefono; item.Activo = dto.Activo;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Estudiantes.FindAsync(id);
        if (item is null) return NotFound();
        if (await db.Matriculas.AnyAsync(m => m.IdEstudiante == id))
            return Conflict("No se puede eliminar: el estudiante tiene matrículas.");
        db.Estudiantes.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
