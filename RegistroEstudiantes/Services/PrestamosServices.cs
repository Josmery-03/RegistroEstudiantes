using Microsoft.EntityFrameworkCore;
using Aplicada1.Core;
using RegistroEstudiantes.Context;
using RegistroEstudiantes.Models;
using System.Linq.Expressions;

namespace RegistroEstudiantes.Services
{
    public class PrestamosServices
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;
        public PrestamosServices(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<bool> Existe(int prestamoId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Prestamos.AnyAsync(p => p.PrestamoId == prestamoId);
        }
        public async Task<bool> LibroPrestado(int libroId, int prestamoIdActual = 0)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Prestamos
                .AnyAsync(p => p.LibroId == libroId && !p.Devuelto && p.PrestamoId != prestamoIdActual);
        }
        private async Task<bool> Insertar(Prestamos prestamo)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Prestamos.Add(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Prestamos prestamo)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Prestamos.Update(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Guardar(Prestamos prestamo)
        {
            if (!await Existe(prestamo.PrestamoId))
                return await Insertar(prestamo);
            else
                return await Modificar(prestamo);
        }
        public async Task<bool> Eliminar(int prestamoId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var prestamo = await contexto.Prestamos.FindAsync(prestamoId);
            if (prestamo == null) return false;

            contexto.Prestamos.Remove(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<Prestamos?> Buscar(int prestamoId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
        }
        public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro)
                .Where(criterio)
                .ToListAsync();
        }
    }
}
