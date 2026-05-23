using PlantCare.Application.Common;
using PlantCare.Application.Common.Hateoas;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Services;

namespace PlantCare.Application.Services;

public class LinkBuilderService : ILinkBuilderService
{
    public Resource<T> BuildResource<T>(T data, params (string rel, string href, string method)[] links)
    {
        return new Resource<T>
        {
            Data = data,
            Links = links.Select(l => new Link { Rel = l.rel, Href = l.href, Method = l.method }).ToList()
        };
    }

    public PagedResource<T> BuildPagedResource<T>(PagedResult<T> paged, PlantaQuery query, string baseUrl)
    {
        var links = new List<Link>
        {
            new() { Rel = "self", Href = $"{baseUrl}/api/planta?{BuildQueryString(query)}", Method = "GET" }
        };

        if (paged.HasPrevious)
        {
            var prevQuery = CloneQuery(query, query.Page - 1);
            links.Add(new Link { Rel = "prev", Href = $"{baseUrl}/api/planta?{BuildQueryString(prevQuery)}", Method = "GET" });
        }

        if (paged.HasNext)
        {
            var nextQuery = CloneQuery(query, query.Page + 1);
            links.Add(new Link { Rel = "next", Href = $"{baseUrl}/api/planta?{BuildQueryString(nextQuery)}", Method = "GET" });
        }

        return new PagedResource<T>
        {
            Data = paged.Items,
            Pagination = new PaginationMetadata
            {
                Page = paged.Page,
                PageSize = paged.PageSize,
                TotalItems = paged.TotalItems,
                TotalPages = paged.TotalPages,
                HasPrevious = paged.HasPrevious,
                HasNext = paged.HasNext
            },
            Links = links
        };
    }

    private static PlantaQuery CloneQuery(PlantaQuery query, int page) => new()
    {
        Page = page,
        PageSize = query.PageSize,
        SortBy = query.SortBy,
        SortDirection = query.SortDirection,
        Nome = query.Nome,
        Especie = query.Especie,
        Status = query.Status,
        UsuarioId = query.UsuarioId
    };

    private static string BuildQueryString(PlantaQuery query)
    {
        var parts = new List<string>
        {
            $"page={query.Page}",
            $"pageSize={query.PageSize}",
            $"sortBy={Uri.EscapeDataString(query.SortBy)}",
            $"sortDirection={Uri.EscapeDataString(query.SortDirection)}"
        };

        if (!string.IsNullOrWhiteSpace(query.Nome))
            parts.Add($"nome={Uri.EscapeDataString(query.Nome)}");
        if (!string.IsNullOrWhiteSpace(query.Especie))
            parts.Add($"especie={Uri.EscapeDataString(query.Especie)}");
        if (!string.IsNullOrWhiteSpace(query.Status))
            parts.Add($"status={Uri.EscapeDataString(query.Status)}");
        if (query.UsuarioId.HasValue)
            parts.Add($"usuarioId={query.UsuarioId.Value}");

        return string.Join("&", parts);
    }
}
