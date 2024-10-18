using CsvHelper.Configuration;
using ProgressSoft.Apolo.Application.DTOs;

namespace ProgressSoft.Apolo.Application.Helpers
{
    public class ExportBusinessCardMap : ClassMap<ExportBusinessCard>
    {
        public ExportBusinessCardMap()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Name).Index(1);
            Map(m => m.Gender).Index(2);
            Map(m => m.BirthOfDate).Index(3);
            Map(m => m.Email).Index(4);
            Map(m => m.Phone).Index(5);
            Map(m => m.Address).Index(6);
            Map(m => m.Image).Index(7);
            Map(m => m.ImageType).Index(8);
        }
    }
}
