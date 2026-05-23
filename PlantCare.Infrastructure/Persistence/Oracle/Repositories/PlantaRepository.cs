using Microsoft.EntityFrameworkCore;
using PlantCare.Application.Common;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Domain.Entities;
using PlantCare.Infrastructure.Persistence.Oracle;

namespace PlantCare.Infrastructure.Persistence.Oracle.Repositories;

public class PlantaRepository : IPlantaRepository
{
    private readonly PlantCareContext _context;

    public PlantaRepository(PlantCareContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Planta>> GetPagedAsync(PlantaQuery query)
    {
        var dbQuery = _context.Plantas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Nome))
            dbQuery = dbQuery.Where(p => p.Nome.Contains(query.Nome));
        if (!string.IsNullOrWhiteSpace(query.Especie))
            dbQuery = dbQuery.Where(p => p.Especie.Contains(query.Especie));
        if (!string.IsNullOrWhiteSpace(query.Status))
            dbQuery = dbQuery.Where(p => p.Status == query.Status);
        if (query.UsuarioId.HasValue)
            dbQuery = dbQuery.Where(p => p.UsuarioId == query.UsuarioId.Value);

        var totalItems = await dbQuery.CountAsync();

        dbQuery = ApplySorting(dbQuery, query.SortBy, query.SortDirection);

        var items = await dbQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedResult<Planta>
        {
            Items = items,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalItems = totalItems
        };
    }

    public async Task<Planta?> GetByIdAsync(int id) =>
        await _context.Plantas.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Planta> AddAsync(Planta planta)
    {
        await _context.Plantas.AddAsync(planta);
        await _context.SaveChangesAsync();
        return planta;
    }

    public async Task UpdateAsync(Planta planta)
    {
        _context.Plantas.Update(planta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var planta = await _context.Plantas.FindAsync(id);
        if (planta is not null)
        {
            _context.Plantas.Remove(planta);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _context.Plantas.AnyAsync(p => p.Id == id);

    private static IQueryable<Planta> ApplySorting(IQueryable<Planta> query, string sortBy, string sortDirection)
    {
        var descending = sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);

        return sortBy.ToLowerInvariant() switch
        {
            "nome" => descending ? query.OrderByDescending(p => p.Nome) : query.OrderBy(p => p.Nome),
            "especie" => descending ? query.OrderByDescending(p => p.Especie) : query.OrderBy(p => p.Especie),
            "status" => descending ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status),
            "datacadastro" => descending ? query.OrderByDescending(p => p.DataCadastro) : query.OrderBy(p => p.DataCadastro),
            _ => descending ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id)
        };
    }
}
