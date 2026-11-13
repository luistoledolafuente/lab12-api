using System.ComponentModel.DataAnnotations;

namespace lab12_api.Models;

public class Estudiante
{
    [Key] public int IdEstudiante { get; set; }
    [Required, MaxLength(100)] public string Nombres { get; set; } = string.Empty;
    [Required, MaxLength(100)] public string Apellidos { get; set; } = string.Empty;
    [Required, EmailAddress, MaxLength(150)] public string Email { get; set; } = string.Empty;
    [MaxLength(20)] public string Telefono { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public bool Activo { get; set; } = true;

    public ICollection<Matricula>? Matriculas { get; set; }
}
