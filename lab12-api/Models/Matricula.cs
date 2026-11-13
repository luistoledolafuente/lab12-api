using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab12_api.Models;

public class Matricula
{
    [Key] public int IdMatricula { get; set; }
    [ForeignKey(nameof(Estudiante))] public int IdEstudiante { get; set; }
    [ForeignKey(nameof(Curso))] public int IdCurso { get; set; }
    public DateTime FechaMatricula { get; set; } = DateTime.Now;
    [MaxLength(30)] public string Estado { get; set; } = "Activa";
    [Column(TypeName = "decimal(10,2)")] public decimal MontoTotal { get; set; }

    public Estudiante? Estudiante { get; set; }
    public Curso? Curso { get; set; }
    public ICollection<Pago>? Pagos { get; set; }
}
