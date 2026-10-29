using SmartDeApi.Common.Endpoints;
using SmartDeApi.Data;
using SmartDeApi.Data.Entities;

namespace SmartDeApi.Features.Campaigns.CreateCampaign;

public sealed record CreateCampaignRequest(string Name, string? Description, decimal DiscountPercentage,
    DateTimeOffset StartsAt, DateTimeOffset EndsAt, string Status = CampaignStatuses.Active);

public sealed class CreateCampaignEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/api/campaigns", async (CreateCampaignRequest request, ApplicationDbContext db, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 200
                || request.Description?.Length > 2000 || request.DiscountPercentage is < 0 or > 100
                || request.EndsAt <= request.StartsAt || !CampaignStatuses.IsValid(request.Status))
                return Results.BadRequest(new { error = "Campaign fields are invalid; discount must be 0–100 and end must follow start." });

            var campaign = new Campaign
            {
                Name = request.Name.Trim(), Description = request.Description,
                DiscountPercentage = request.DiscountPercentage, StartsAt = request.StartsAt.ToUniversalTime(),
                EndsAt = request.EndsAt.ToUniversalTime(), Status = request.Status.Trim()
            };
            db.Campaigns.Add(campaign);
            await db.SaveChangesAsync(ct);
            return Results.Created($"/api/campaigns/{campaign.Id}", campaign);
        }).WithName("CreateCampaign").WithTags("Campaigns");
}
