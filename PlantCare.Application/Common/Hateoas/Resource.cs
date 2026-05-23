namespace PlantCare.Application.Common.Hateoas;

public class Resource<T>
{
    public T Data { get; set; } = default!;
    public IList<Link> Links { get; set; } = new List<Link>();
}
