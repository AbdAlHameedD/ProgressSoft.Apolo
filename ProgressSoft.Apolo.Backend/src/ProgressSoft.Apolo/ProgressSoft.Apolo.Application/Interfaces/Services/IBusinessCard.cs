namespace ProgressSoft.Apolo.Application;

public interface IBusinessCardService : IBaseService<BusinessCardModel>
{
    Result<BusinessCardModel> Add(AddBusinessCardCommand command);
    Result<BusinessCardModel> Edit(BusinessCardModel model);
    Result<IEnumerable<BusinessCardModel>> GetAll(BusinessCardFilter filter);
    Result<BusinessCardModel> GetById(int id);
}