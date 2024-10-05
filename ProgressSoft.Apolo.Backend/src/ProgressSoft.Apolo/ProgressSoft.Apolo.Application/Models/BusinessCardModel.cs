using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public class BusinessCardModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Gender Gender { get; set; }
    public required DateOnly BirthOfDate { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public string? Photo { get; set; }
    public required string Address { get; set; }
}
