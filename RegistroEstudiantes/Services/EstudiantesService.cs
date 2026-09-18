using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Aplicada1.Core;
using RegistroEstudiantes.Context;

using RegistroEstudiantes.Models;
using Microsoft.AspNetCore.Mvc;

namespace RegistroEstudiantes.Services
{
    public class EstudiantesService(IDbContextFactory<Contexto> DbFactory) : IService<Estudiantes, int>
    {
        
        public async Task<bool> Guardar(Estudiantes estudiante)
        {
            if (!await Existe(estudiante.EstudianteId))
            {
                return await Insertar(estudiante);
            }
            else
            {
                return await Modificar(estudiante);
            }

        }

        private async Task<bool> Insertar(Estudiantes estudiante)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Estudiantes.Add(estudiante);
            return await contexto.SaveChangesAsync() > 0;
        }
   
        private async Task<bool> Modificar(Estudiantes estudiante)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Estudiantes.Update(estudiante);
            return await contexto
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int estudianteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Estudiantes
                 .Where(e => e.EstudianteId == estudianteId)
                 .ExecuteDeleteAsync() > 0;
        }

        public async Task<Estudiantes?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Estudiantes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EstudianteId == id);
        }

       
        public async Task<List<Estudiantes>> GetList(Expression<Func<Estudiantes, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Estudiantes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
        }

       
        private async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Estudiantes
                .AnyAsync(e => e.EstudianteId == id);
        }

        
        public async Task<bool> ExisteNombre (string nombres, int id = 0) 
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Estudiantes
                .AnyAsync(e => e.Nombres.ToLower() == nombres.ToLower() && e.EstudianteId != id);

        }


        
    
    }
}
