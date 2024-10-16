using System.Xml;

namespace ProgressSoft.Apolo.Application;

public interface IBusinessCardService : IBaseService<BusinessCardModel>
{
    Result<BusinessCardModel> Add(AddBusinessCardCommand command);
    Result<BusinessCardModel> Edit(BusinessCardModel model);
    Result<IEnumerable<BusinessCardModel>> GetAll(BusinessCardFilter filter);
    Result<BusinessCardModel> GetById(int id);
    MemoryStream ExportCSV(BusinessCardFilter filter);
    MemoryStream ExportXML(BusinessCardFilter filter);
}