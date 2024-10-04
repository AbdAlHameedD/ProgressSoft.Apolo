using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure;

public class BusinessCardRepository : BaseRepository<BusinessCard>, IBusinessCardRepository
{
    private readonly ApoloDbContext _apoloDbContext;

    public BusinessCardRepository(ApoloDbContext apoloDbContext) : base(apoloDbContext) {
        _apoloDbContext = apoloDbContext;
    }

    /// <summary>
    /// Delete business card from database
    /// </summary>
    /// <param name="id">Business card identity</param>
    /// <returns>Deleted business card</returns>
    public Result<BusinessCard> Delete(int id)
    {
        try
        {
            BusinessCard? entity = (from businessCard in _apoloDbContext.BusinessCards
                                                 where businessCard.Id == id
                                                 select businessCard).FirstOrDefault();

            if (entity is not null) 
            {
                _apoloDbContext.Remove(entity);
            }

            return new Result<BusinessCard>
            {
                Status = OperationStatus.Success,
                Data = entity
            };
        }
        catch (Exception ex)
        {
            return new Result<BusinessCard>
            {
                Status = OperationStatus.Failed,
                Message = ex.Message
            };
        }
    }

    /// <summary>
    /// Get all business cards that stored in the database
    /// </summary>
    /// <returns>All business cards</returns>
    public Result<IQueryable<BusinessCard>> Get()
    {
        try 
        {
            IQueryable<BusinessCard> businessCards = from businessCard in _apoloDbContext.BusinessCards
                                                     select businessCard;

            return new Result<IQueryable<BusinessCard>> 
            {
                Status = OperationStatus.Success,
                Data = businessCards
            };
        } 
        catch (Exception ex) 
        {
            return new Result<IQueryable<BusinessCard>>
            {
                Status = OperationStatus.Failed,
                Message = ex.Message
            };
        }
    }

    /// <summary>
    /// Insert new business card in the database
    /// </summary>
    /// <param name="entity">Represent business card to insert</param>
    /// <returns>Inserted entity</returns>
    public Result<BusinessCard> Insert(BusinessCard entity)
    {
        try
        {
            _apoloDbContext.Add(entity);
            bool isInserted = _apoloDbContext.SaveChangesAsync().Result > 0;

            return new Result<BusinessCard>
            {
                Status = OperationStatus.Success,
                Data = entity
            };
        }
        catch (Exception ex)
        {
            return new Result<BusinessCard>
            {
                Status = OperationStatus.Failed,
                Message = ex.Message
            };
        }
    }
}
