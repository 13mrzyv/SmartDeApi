using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Orders.UpdateOrder;

public sealed record UpdateOrderRequest(string CustomerName, string DeliveryAddress, string Status, decimal TotalAmount);

public sealed class UpdateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/api/orders/{id:long}", async (long id, UpdateOrderRequest request, ApplicationDbContext db, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName) || request.CustomerName.Length > 200
                || string.IsNullOrWhiteSpace(request.DeliveryAddress) || request.DeliveryAddress.Length > 500
                || string.IsNullOrWhiteSpace(request.Status) || request.Status.Length > 30 || request.TotalAmount < 0)
                return Results.BadRequest(new { error = "Customer name, delivery address, status, or total amount is invalid." });

            var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (order is null) return Results.NotFound();
            order.CustomerName = request.CustomerName.Trim();
            order.DeliveryAddress = request.DeliveryAddress.Trim();
            order.Status = request.Status.Trim();
            order.TotalAmount = request.TotalAmount;
            await db.SaveChangesAsync(ct);
            return Results.Ok(order);
        }).WithName("UpdateOrder").WithTags("Orders");
}
