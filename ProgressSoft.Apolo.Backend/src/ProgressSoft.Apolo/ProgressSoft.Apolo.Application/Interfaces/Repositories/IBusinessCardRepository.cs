using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public interface IBusinessCardRepository : IBaseRepository<BusinessCard>
{
    Result<IQueryable<BusinessCard>> Get(BusinessCardFilter filter);
    Result<BusinessCard> FindById(int id);
}
