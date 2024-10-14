using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public required byte[] EncodedImage { get; set; }
        public required string Type { get; set; }

        public List<BusinessCard> BusinessCards { get; set; } = [];
    }
}
