using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiantes.Models;
public class EntradasDetalle {

    [Key]
    public int DetalleId { get; set; }
    public int EntradaId { get; set; }
    public int LibroId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero")]
    public int Cantidad { get; set; }

    [ForeignKey("EntradaId")]
    [InverseProperty("EntradasDetalle")]
    public virtual Entradas Entrada { get; set; } = null!;

    [ForeignKey("LibroId")]
    public virtual Libros Libro {  get; set; }
}
