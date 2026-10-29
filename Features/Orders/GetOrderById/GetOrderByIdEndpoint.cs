using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Orders.GetOrderById;

public sealed class GetOrderByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/api/orders/{id:long}", async (long id, ApplicationDbContext db, CancellationToken ct) =>
        {
            var order = await db.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            return order is null ? Results.NotFound() : Results.Ok(order);
        }).WithName("GetOrderById").WithTags("Orders");
}
