namespace SmartDeApi.Data.Entities;

public static class CampaignStatuses
{
    public const string Active = "Active";
    public const string Inactive = "Inactive";
    public const string Expired = "Expired";

    public static bool IsValid(string? status) =>
        status is Active or Inactive or Expired;
}
