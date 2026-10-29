using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;
using SmartDeApi.Data.Entities;

namespace SmartDeApi.Features.Products.CreateProduct;

public sealed record CreateProductRequest(string Name, string? Description, decimal Price, int StockQuantity);

public sealed class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/api/products", async (CreateProductRequest request, ApplicationDbContext db, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 200 || request.Price < 0 || request.StockQuantity < 0)
                return Results.BadRequest(new { error = "Name must be provided (max 200 chars); price and stock cannot be negative." });
            if (request.Description?.Length > 2000)
                return Results.BadRequest(new { error = "Description cannot exceed 2000 characters." });

            var product = new Product { Name = request.Name.Trim(), Description = request.Description, Price = request.Price, StockQuantity = request.StockQuantity };
            db.Products.Add(product);
            await db.SaveChangesAsync(ct);
            return Results.Created($"/api/products/{product.Id}", product);
        }).WithName("CreateProduct").WithTags("Products");
}
