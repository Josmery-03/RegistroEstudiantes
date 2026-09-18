using Microsoft.EntityFrameworkCore;
using RegistroEstudiantes.Models;

namespace RegistroEstudiantes.Context
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Estudiantes> Estudiantes { get; set; }
    }
}
 