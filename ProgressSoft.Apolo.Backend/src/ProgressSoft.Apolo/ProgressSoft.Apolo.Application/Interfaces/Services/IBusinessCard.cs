using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public interface IBusinessCardService : IBaseService<BusinessCardModel>
{
    Result<BusinessCardModel> Add(AddBusinessCardCommand command);
    Result<BusinessCardModel> Edit(BusinessCardModel model);
}