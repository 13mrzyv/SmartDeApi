using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Campaigns.ListCampaigns;

public sealed class ListCampaignsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/api/campaigns", async (ApplicationDbContext db, CancellationToken ct) =>
            Results.Ok(await db.Campaigns.AsNoTracking().OrderBy(x => x.Id).ToListAsync(ct)))
            .WithName("ListCampaigns").WithTags("Campaigns");

}
