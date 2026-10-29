using Microsoft.EntityFrameworkCore;
using SmartDeApi.Data;
using SmartDeApi.Data.Entities;

namespace SmartDeApi.Features.Campaigns.ExpireCampaigns;

public sealed class ExpireCampaignsJob(
    ApplicationDbContext db,
    ILogger<ExpireCampaignsJob> logger)
{
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var expiredCampaigns = await db.Campaigns
            .Where(campaign => campaign.Status == CampaignStatuses.Active && campaign.EndsAt <= now)
            .ToListAsync(cancellationToken);

        foreach (var campaign in expiredCampaigns)
            campaign.Status = CampaignStatuses.Expired;

        await db.SaveChangesAsync(cancellationToken);

        if (expiredCampaigns.Count > 0)
            logger.LogInformation("Expired {CampaignCount} campaign(s).", expiredCampaigns.Count);
    }
}
