using Microsoft.EntityFrameworkCore;
using RegistroEstudiantes.Context;
using RegistroEstudiantes.Models;
using System.Linq.Expressions;

namespace RegistroEstudiantes.Services
{
        public class LibrosServices(IDbContextFactory<Contexto> DbFactory)
        {
            // Metodo Existe
            private async Task<bool> Existe(int libroId)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Libros
                    .AnyAsync(l => l.LibroId == libroId);
            }

            // Metodo "Libro duplicado"

            public async Task<bool> ExtisteTitulo(String titulo, int libroId = 0)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Libros
                    .AnyAsync(l => l.Titulo.ToLower() == titulo.ToLower() && l.LibroId != libroId);
            }

            // Metodo insertar
            private async Task<bool> Insertar(Libros libro)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                contexto.Libros.Add(libro);
                return await contexto.SaveChangesAsync() > 0;
            }

            // Metodo modificar

            private async Task<bool> Modificar(Libros libro)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                contexto.Update(libro);
                return await contexto
                    .SaveChangesAsync() > 0;
            }

            // Metodo guardar

            public async Task<bool> Guardar(Libros libro)
            {
                if (!await Existe(libro.LibroId))
                    return await Insertar(libro);
                else
                {
                    return await Modificar(libro);
                }
            }

            // Metodo buscar 

            public async Task<Libros?> Buscar(int libroId)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Libros
                    .FirstOrDefaultAsync(l => l.LibroId == libroId);
            }

            //Metodo eliminar

            public async Task<bool> Eliminar(int libroId)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Libros
                    .AsNoTracking()
                    .Where(l => l.LibroId == libroId)
                    .ExecuteDeleteAsync() > 0;
            }

            // Metodo listar

            public async Task<List<Libros>> Listar(Expression<Func<Libros, bool>> criterio)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Libros
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
            }
        }
    }
