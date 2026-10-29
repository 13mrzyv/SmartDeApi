using Hangfire;

namespace SmartDeApi.Features.Campaigns.ExpireCampaigns;

public static class CampaignRecurringJobsExtensions
{
    public static IApplicationBuilder RegisterCampaignRecurringJobs(this IApplicationBuilder app)
    {
        RecurringJob.AddOrUpdate<ExpireCampaignsJob>(
            recurringJobId: "campaigns-expire-daily",
            methodCall: job => job.ExecuteAsync(CancellationToken.None),
            cronExpression: Cron.Daily(),
            options: new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });

        return app;
    }
}
