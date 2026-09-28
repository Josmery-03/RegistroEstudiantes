using System.ComponentModel.DataAnnotations;
    namespace RegistroEstudiantes.Models
    {
        public class Libros
        {
            [Key]
            public int LibroId { get; set; }

            [Required(ErrorMessage = "Este campo es requerido")]
            public string Titulo { get; set; } = string.Empty;

            [Required(ErrorMessage = "Este campo es requerido")]
            public string Autor { get; set; } = string.Empty;

            [Required(ErrorMessage = "Este campo es requerido")]
            public int AnoPublicacion
            {
                get; set;
            }
        }
    }
