using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiantes.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage = "Debes de seleccionar a un estudiante")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar a un estudiante")]
        public int EstudianteId { get; set; }


        [Required(ErrorMessage = "Debes de seleccionar a un libro")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar a un libro")]
        public int LibroId { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaPrestamo {  get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha de devolucion es necesaria")]
        [DataType(DataType.Date)]
        public DateTime FechaDevolucion { get; set; } = DateTime.Now.AddDays(7);

        public bool Devuelto { get; set; } = false;

        [ForeignKey("EstudianteId")]
        public virtual Estudiantes? Estudiante { get; set; }

        [ForeignKey("LibroId")]
        public virtual Libros? Libro { get; set; }
    }
}

