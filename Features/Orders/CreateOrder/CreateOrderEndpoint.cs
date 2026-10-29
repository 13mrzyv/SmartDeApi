using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;
using SmartDeApi.Data.Entities;

namespace SmartDeApi.Features.Orders.CreateOrder;

public sealed record CreateOrderRequest(string CustomerName, string DeliveryAddress, decimal TotalAmount, string Status = "Pending");

public sealed class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/api/orders", async (CreateOrderRequest request, ApplicationDbContext db, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName) || request.CustomerName.Length > 200
                || string.IsNullOrWhiteSpace(request.DeliveryAddress) || request.DeliveryAddress.Length > 500
                || string.IsNullOrWhiteSpace(request.Status) || request.Status.Length > 30 || request.TotalAmount < 0)
                return Results.BadRequest(new { error = "Customer name, delivery address, status, or total amount is invalid." });

            var order = new DeliveryOrder
            {
                CustomerName = request.CustomerName.Trim(), DeliveryAddress = request.DeliveryAddress.Trim(),
                Status = request.Status.Trim(), TotalAmount = request.TotalAmount
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync(ct);
            return Results.Created($"/api/orders/{order.Id}", order);
        }).WithName("CreateOrder").WithTags("Orders");
}
