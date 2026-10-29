using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Products.UpdateProduct;

public sealed record UpdateProductRequest(string Name, string? Description, decimal Price, int StockQuantity);

public sealed class UpdateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/api/products/{id:int}", async (int id, UpdateProductRequest request, ApplicationDbContext db, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 200 || request.Price < 0 || request.StockQuantity < 0)
                return Results.BadRequest(new { error = "Name must be provided (max 200 chars); price and stock cannot be negative." });
            if (request.Description?.Length > 2000)
                return Results.BadRequest(new { error = "Description cannot exceed 2000 characters." });

            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (product is null) return Results.NotFound();
            product.Name = request.Name.Trim();
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            await db.SaveChangesAsync(ct);
            return Results.Ok(product);
        }).WithName("UpdateProduct").WithTags("Products");
}
