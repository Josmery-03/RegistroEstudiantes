using Aplicada1.Core;
using RegistroEstudiantes.Context;
using RegistroEstudiantes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroEstudiantes.Services;
public class DevolucionesService(
    IDbContextFactory<Contexto> dbFactory
) : Aplicada1.Core.IServiceResult<Devoluciones, int>
{
    public async Task<Result> Guardar(Devoluciones devolucion)
    {
        if (!await Existe(devolucion.DevolucionesId))
        {
            var inserto = await Insertar(devolucion);
            return Result.Success(inserto);
        }
        else
        {
            var modifico = await Modificar(devolucion);
            return Result.Success(modifico);
        }
    }
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Devoluciones.AnyAsync(d => d.DevolucionesId == id);
    }

    private async Task<bool> Insertar(Devoluciones devolucion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Devoluciones.Add(devolucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Devoluciones devolucion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Devoluciones.Update(devolucion);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<Result<Devoluciones?>> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Devoluciones
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DevolucionesId == id);
    }
    public async Task<Result<List<Devoluciones>>> GetList(Expression<Func<Devoluciones, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Devoluciones
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Result> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var eliminado = await contexto.Devoluciones
            .Where(d => d.DevolucionesId == id)
            .ExecuteDeleteAsync() > 0;
        return Result.Success(eliminado);
    }
    public async Task<double> ObtenerPromedioDiasPrestado(int libroId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var devoluciones = await contexto.Devoluciones
            .Where(d => d.LibroId == libroId)
            .ToListAsync();

        if (!devoluciones.Any())
            return 0;

        return devoluciones.Average(d => d.DiasPrestados);
    }
}
