using System.Runtime.CompilerServices;
using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure;

public class BusinessCardRepository : BaseRepository<BusinessCard>, IBusinessCardRepository
{
    private readonly ApoloDbContext _apoloDbContext;
    private readonly ResultHelper _resultHelper;

    public BusinessCardRepository(ApoloDbContext apoloDbContext, ResultHelper resultHelper) : base(apoloDbContext, resultHelper) {
        _apoloDbContext = apoloDbContext;
        _resultHelper = resultHelper;
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

            return _resultHelper.GenerateSuccessResult<BusinessCard>(entity);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCard>(ex);
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

            return _resultHelper.GenerateSuccessResult<IQueryable<BusinessCard>>(businessCards);
        } 
        catch (Exception ex) 
        {
            return _resultHelper.GenerateFailedResult<IQueryable<BusinessCard>>(ex);
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

            return _resultHelper.GenerateSuccessResult<BusinessCard>(entity);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCard>(ex);
        }
    }
}
