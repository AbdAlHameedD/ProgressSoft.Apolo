using ProgressSoft.Apolo.Application.DTOs;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application;

public interface IBusinessCardRepository : IBaseRepository<BusinessCard>
{
    Result<IQueryable<BusinessCard>> Get(BusinessCardFilter filter);
    Result<BusinessCard> FindById(int id);
    Result<IQueryable<ExportBusinessCard>> GetForExport(BusinessCardFilter filter);
    Result<IEnumerable<BusinessCard>> InsertBulk(IEnumerable<BusinessCard> businessCards);
}
