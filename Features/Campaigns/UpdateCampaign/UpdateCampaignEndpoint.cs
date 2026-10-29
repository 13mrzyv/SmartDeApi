using Microsoft.EntityFrameworkCore;
using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;
using SmartDeApi.Data.Entities;

namespace SmartDeApi.Features.Campaigns.UpdateCampaign;

public sealed record UpdateCampaignRequest(string Name, string? Description, decimal DiscountPercentage,
    DateTimeOffset StartsAt, DateTimeOffset EndsAt, string Status);

public sealed class UpdateCampaignEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/api/campaigns/{id:int}", async (int id, UpdateCampaignRequest request, ApplicationDbContext db, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 200
                || request.Description?.Length > 2000 || request.DiscountPercentage is < 0 or > 100
                || request.EndsAt <= request.StartsAt || !CampaignStatuses.IsValid(request.Status))
                return Results.BadRequest(new { error = "Campaign fields are invalid; discount must be 0–100 and end must follow start." });

            var campaign = await db.Campaigns.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (campaign is null) return Results.NotFound();
            campaign.Name = request.Name.Trim();
            campaign.Description = request.Description;
            campaign.DiscountPercentage = request.DiscountPercentage;
            campaign.StartsAt = request.StartsAt.ToUniversalTime();
            campaign.EndsAt = request.EndsAt.ToUniversalTime();
            campaign.Status = request.Status.Trim();
            await db.SaveChangesAsync(ct);
            return Results.Ok(campaign);
        }).WithName("UpdateCampaign").WithTags("Campaigns");
}
