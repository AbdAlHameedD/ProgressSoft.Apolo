namespace ProgressSoft.Apolo.Domain;

public class Image
{
    public int Id { get; init; }
    public required byte[] EncodedImage { get; init; }
    public required string Type { get; init; }

    public ICollection<BusinessCard> BusinessCards { get; set; } = [];
}
