using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Products.ListProducts;

public sealed class ListProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/api/products", async (ApplicationDbContext db, CancellationToken ct) =>
            Results.Ok(await db.Products.AsNoTracking().OrderBy(x => x.Id).ToListAsync(ct)))
            .WithName("ListProducts").WithTags("Products");
}
