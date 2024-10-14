namespace ProgressSoft.Apolo.Domain;

public class BusinessCard
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required Gender Gender { get; init; }
    public required DateOnly BirthOfDate { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public int? ImageId { get; init; }
    public required string Address { get; init; }

    public Image? Image { get; init; }
}
