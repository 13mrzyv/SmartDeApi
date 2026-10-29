using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Campaigns.GetCampaignById;

public sealed class GetCampaignByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/api/campaigns/{id:int}", async (int id, ApplicationDbContext db, CancellationToken ct) =>
        {
            var campaign = await db.Campaigns.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            return campaign is null ? Results.NotFound() : Results.Ok(campaign);
        }).WithName("GetCampaignById").WithTags("Campaigns");
}
