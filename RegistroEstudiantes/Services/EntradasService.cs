using Microsoft.EntityFrameworkCore;
using RegistroEstudiantes.Context;
using RegistroEstudiantes.Models;
using System.Linq.Expressions;

namespace RegistroEstudiantes.Services;
public class EntradasService(IDbContextFactory<Contexto> DbFactory) : IEntradasService
{
    private async Task<bool> Existe(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entradas.AnyAsync(e => e.EntradaId == entradaId);
    }
    private async Task<bool> Insertar(Entradas entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var detalle in entrada.EntradasDetalle)
        {
            var libro = await contexto.Libros.FindAsync(detalle.LibroId);
            if (libro != null)
            {
                libro.CantidadDisponible += detalle.Cantidad;
            }
        }

        contexto.Entradas.Add(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Entradas entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entradaAnterior = await contexto.Entradas
            .Include(e => e.EntradasDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if (entradaAnterior != null)
        {
            foreach (var detalleViejo in entradaAnterior.EntradasDetalle)
            {
                var libro = await contexto.Libros.FindAsync(detalleViejo.LibroId);
                if (libro != null)
                {
                    libro.CantidadDisponible -= detalleViejo.Cantidad;
                }
            }

            var detallesAnteriores = contexto.EntradasDetalle.Where(d => d.EntradaId == entrada.EntradaId);
            contexto.EntradasDetalle.RemoveRange(detallesAnteriores);
        }

        foreach (var detalleNuevo in entrada.EntradasDetalle)
        {
            var libro = await contexto.Libros.FindAsync(detalleNuevo.LibroId);
            if (libro != null)
            {
                libro.CantidadDisponible += detalleNuevo.Cantidad;
            }
        }
        contexto.Update(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Entradas entrada)
    {
        if (!await Existe(entrada.EntradaId))
        {
            return await Insertar(entrada);
        }
        else
        {
            return await Modificar(entrada);
        }
    }
    public async Task<bool> Eliminar(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entrada = await contexto.Entradas
            .Include(e => e.EntradasDetalle)
            .FirstOrDefaultAsync(e => e.EntradaId == entradaId);

        if (entrada == null) return false;

        foreach (var detalle in entrada.EntradasDetalle)
        {
            var libro = await contexto.Libros.FindAsync(detalle.LibroId);
            if (libro != null)
            {
                libro.CantidadDisponible -= detalle.Cantidad;
            }
        }

        contexto.Entradas.Remove(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<Entradas?> Buscar(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entradas
            .Include(e => e.EntradasDetalle)
            .ThenInclude(d => d.Libro)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
    }
    public async Task<List<Entradas>> GetList(Expression<Func<Entradas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entradas
            .Include(e => e.EntradasDetalle)
            .ThenInclude(d => d.Libro)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
