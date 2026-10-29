using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Products.DeleteProduct;

public sealed class DeleteProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/api/products/{id:int}", async (int id, ApplicationDbContext db, CancellationToken ct) =>
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (product is null) return Results.NotFound();
            db.Products.Remove(product);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        }).WithName("DeleteProduct").WithTags("Products");
}
