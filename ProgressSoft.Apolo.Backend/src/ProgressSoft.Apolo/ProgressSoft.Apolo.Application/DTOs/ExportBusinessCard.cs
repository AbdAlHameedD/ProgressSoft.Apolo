namespace ProgressSoft.Apolo.Application.DTOs
{
    public class ExportBusinessCard : ExternalBusinessCard
    {
        public required string Image { get; init; }
        public required string ImageType { get; init; }
    }
}
