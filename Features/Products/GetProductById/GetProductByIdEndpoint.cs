using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Products.GetProductById;

public sealed class GetProductByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/api/products/{id:int}", async (int id, ApplicationDbContext db, CancellationToken ct) =>
        {
            var product = await db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            return product is null ? Results.NotFound() : Results.Ok(product);
        }).WithName("GetProductById").WithTags("Products");
}
