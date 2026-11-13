using lab12_api.Models;
using Microsoft.EntityFrameworkCore;

namespace lab12_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Instructor> Instructores => Set<Instructor>();
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
    public DbSet<Pago> Pagos => Set<Pago>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Evita multiple cascade paths en SQL Server
        b.Entity<Curso>().HasOne(c => c.Instructor).WithMany(i => i.Cursos)
            .HasForeignKey(c => c.IdInstructor).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Matricula>().HasOne(m => m.Estudiante).WithMany(e => e.Matriculas)
            .HasForeignKey(m => m.IdEstudiante).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Matricula>().HasOne(m => m.Curso).WithMany(c => c.Matriculas)
            .HasForeignKey(m => m.IdCurso).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Pago>().HasOne(p => p.Matricula).WithMany(m => m.Pagos)
            .HasForeignKey(p => p.IdMatricula).OnDelete(DeleteBehavior.Restrict);
    }
}
