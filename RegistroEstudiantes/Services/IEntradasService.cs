using RegistroEstudiantes.Models;
using System.Linq.Expressions;

namespace RegistroEstudiantes.Services;
public interface IEntradasService
{
    Task<bool> Guardar(Entradas entrada);
    Task<Entradas?> Buscar(int EntradaId);
    Task<bool> Eliminar (int EntradaId);
    Task<List<Entradas>> GetList(Expression<Func<Entradas, bool>> criterio);
}
