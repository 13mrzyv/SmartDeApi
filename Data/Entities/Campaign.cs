namespace SmartDeApi.Data.Entities;

public sealed class Campaign
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTimeOffset StartsAt { get; set; }
    public DateTimeOffset EndsAt { get; set; }
    public required string Status { get; set; } = CampaignStatuses.Active;
}
