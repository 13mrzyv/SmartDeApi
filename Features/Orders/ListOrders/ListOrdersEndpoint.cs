using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Orders.ListOrders;

public sealed class ListOrdersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/api/orders", async (ApplicationDbContext db, CancellationToken ct) =>
            Results.Ok(await db.Orders.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync(ct)))
            .WithName("ListOrders").WithTags("Orders");
}
