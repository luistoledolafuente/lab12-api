using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab12_api.Models;

public class Pago
{
    [Key] public int IdPago { get; set; }
    [ForeignKey(nameof(Matricula))] public int IdMatricula { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Monto { get; set; }
    public DateTime FechaPago { get; set; } = DateTime.Now;
    [MaxLength(50)] public string MetodoPago { get; set; } = string.Empty;
    [MaxLength(30)] public string EstadoPago { get; set; } = "Pagado";

    public Matricula? Matricula { get; set; }
}
