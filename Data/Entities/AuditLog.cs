namespace SmartDeApi.Data.Entities;

public sealed class AuditLog
{
    public long Id { get; set; }
    public required string Message { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
