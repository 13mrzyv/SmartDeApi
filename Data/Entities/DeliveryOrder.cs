namespace SmartDeApi.Data.Entities;

public sealed class DeliveryOrder
{
    public long Id { get; set; }
    public required string CustomerName { get; set; }
    public required string DeliveryAddress { get; set; }
    public required string Status { get; set; } = "Pending";
    public decimal TotalAmount { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
