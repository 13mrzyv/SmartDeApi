using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Orders.DeleteOrder;

public sealed class DeleteOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/api/orders/{id:long}", async (long id, ApplicationDbContext db, CancellationToken ct) =>
        {
            var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (order is null) return Results.NotFound();
            db.Orders.Remove(order);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        }).WithName("DeleteOrder").WithTags("Orders");
}
