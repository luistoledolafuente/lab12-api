using System.ComponentModel.DataAnnotations;

namespace lab12_api.Dtos;

public class InstructorDto
{
    [Required, MaxLength(100)] public string Nombres { get; set; } = string.Empty;
    [Required, MaxLength(100)] public string Apellidos { get; set; } = string.Empty;
    [MaxLength(100)] public string Especialidad { get; set; } = string.Empty;
    [Required, EmailAddress, MaxLength(150)] public string Email { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}
