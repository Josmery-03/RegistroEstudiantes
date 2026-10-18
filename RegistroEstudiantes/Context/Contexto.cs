using Microsoft.EntityFrameworkCore;
using RegistroEstudiantes.Models;

namespace RegistroEstudiantes.Context;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Estudiantes> Estudiantes { get; set; }
    public virtual DbSet<Libros> Libros { get; set; }
    public DbSet<Prestamos> Prestamos { get; set; }
    public DbSet<Devoluciones> Devoluciones { get; set; }
    public virtual DbSet<Entradas> Entradas { get; set; }
    public virtual DbSet<EntradasDetalle> EntradasDetalle { get; set; }
   
}
