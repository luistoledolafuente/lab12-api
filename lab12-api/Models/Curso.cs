using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab12_api.Models;

public class Curso
{
    [Key] public int IdCurso { get; set; }
    [Required, MaxLength(150)] public string Nombre { get; set; } = string.Empty;
    [MaxLength(500)] public string Descripcion { get; set; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")] public decimal Precio { get; set; }
    public int DuracionHoras { get; set; }
    [ForeignKey(nameof(Instructor))] public int IdInstructor { get; set; }
    public bool Activo { get; set; } = true;

    public Instructor? Instructor { get; set; }
    public ICollection<Matricula>? Matriculas { get; set; }
}
