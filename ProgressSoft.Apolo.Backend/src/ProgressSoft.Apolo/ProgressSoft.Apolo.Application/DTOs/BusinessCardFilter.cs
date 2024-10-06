using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public class BusinessCardFilter
{
    public string? Name { get; init; }
    public Gender? Gender { get; init; }
    public DateOnly? FromBirthDate { get; init; }
    public DateOnly? ToBirthDate { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
}
