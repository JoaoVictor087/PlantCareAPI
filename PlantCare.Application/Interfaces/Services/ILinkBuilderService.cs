using PlantCare.Application.Common;
using PlantCare.Application.Common.Hateoas;
using PlantCare.Application.DTOs;

namespace PlantCare.Application.Interfaces.Services;

public interface ILinkBuilderService
{
    Resource<T> BuildResource<T>(T data, params (string rel, string href, string method)[] links);
    PagedResource<T> BuildPagedResource<T>(PagedResult<T> paged, PlantaQuery query, string baseUrl);
}
