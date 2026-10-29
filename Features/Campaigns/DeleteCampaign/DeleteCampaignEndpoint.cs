using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;

namespace SmartDeApi.Features.Campaigns.DeleteCampaign;

public sealed class DeleteCampaignEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/api/campaigns/{id:int}", async (int id, ApplicationDbContext db, CancellationToken ct) =>
        {
            var campaign = await db.Campaigns.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (campaign is null) return Results.NotFound();
            db.Campaigns.Remove(campaign);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        }).WithName("DeleteCampaign").WithTags("Campaigns");
}
