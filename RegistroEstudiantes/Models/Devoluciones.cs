using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace RegistroEstudiantes.Models
{
    public class Devoluciones
    {
        [Key]
        public int DevolucionesId { get; set; }

        [Required(ErrorMessage = "El prestamo deber ser obligatorio")]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage = "El libro deber ser obligatorio")]
        public int LibroId {  get; set; }
        public DateTime FachaDevolucion {  get; set; } = DateTime.Now;
        public int DiasPrestados { get; set; }

    }
}
