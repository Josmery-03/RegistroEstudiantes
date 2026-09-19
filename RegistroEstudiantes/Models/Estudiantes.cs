using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiantes.Models
{
    public class Estudiantes
    {
        [Key]
        public int EstudianteId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "La direccion es requerida")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Email es requerido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [Column("FechaNacimeinto")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
    }
}
