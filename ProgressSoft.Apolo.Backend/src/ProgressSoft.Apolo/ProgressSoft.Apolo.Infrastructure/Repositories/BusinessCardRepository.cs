using Microsoft.EntityFrameworkCore;
using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Application.DTOs;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure;

public class BusinessCardRepository : BaseRepository<BusinessCard>, IBusinessCardRepository
{
    private readonly ApoloDbContext _apoloDbContext;
    private readonly ResultHelper _resultHelper;

    public BusinessCardRepository(ApoloDbContext apoloDbContext, ResultHelper resultHelper) : base(apoloDbContext, resultHelper)
    {
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

                bool isDeleted = _apoloDbContext.SaveChangesAsync().Result > 0;
            }

            return _resultHelper.GenerateSuccessResult<BusinessCard>(entity);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCard>(ex);
        }
    }

    /// <summary>
    /// Get business card that match target id
    /// </summary>
    /// <param name="id">Business card identity</param>
    /// <returns>Matched entity</returns>
    public Result<BusinessCard> FindById(int id)
    {
        try
        {
            BusinessCard? entity = (from businessCard in _apoloDbContext.BusinessCards
                                    where businessCard.Id == id
                                    select businessCard).First();

            return _resultHelper.GenerateSuccessResult(entity);
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
    public Result<IQueryable<BusinessCard>> Get(BusinessCardFilter filter)
    {
        try
        {
            IQueryable<BusinessCard> businessCards = from businessCard in _apoloDbContext.BusinessCards.Include(bc => bc.Image)
                                                     where (filter.Name == null || businessCard.Name.Contains(filter.Name)) &&
                                                           (filter.Gender == null || businessCard.Gender == filter.Gender) &&
                                                           (filter.Email == null || businessCard.Email.Contains(filter.Email)) &&
                                                           (filter.Phone == null || businessCard.Phone.Contains(filter.Phone)) &&
                                                           (filter.FromBirthDate == null || businessCard.BirthOfDate >= filter.FromBirthDate) &&
                                                           (filter.ToBirthDate == null || businessCard.BirthOfDate <= filter.ToBirthDate)
                                                     select businessCard;

            return _resultHelper.GenerateSuccessResult(businessCards);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<IQueryable<BusinessCard>>(ex);
        }
    }

    public Result<IQueryable<BusinessCardExport>> GetForExport(BusinessCardFilter filter)
    {
        try
        {
            IQueryable<BusinessCardExport> businessCards = from businessCard in _apoloDbContext.BusinessCards.Include(bc => bc.Image)
                                                           where (filter.Name == null || businessCard.Name.Contains(filter.Name)) &&
                                                                 (filter.Gender == null || businessCard.Gender == filter.Gender) &&
                                                                 (filter.Email == null || businessCard.Email.Contains(filter.Email)) &&
                                                                 (filter.Phone == null || businessCard.Phone.Contains(filter.Phone)) &&
                                                                 (filter.FromBirthDate == null || businessCard.BirthOfDate >= filter.FromBirthDate) &&
                                                                 (filter.ToBirthDate == null || businessCard.BirthOfDate <= filter.ToBirthDate)
                                                           select new BusinessCardExport
                                                           {
                                                               Address = businessCard.Address,
                                                               BirthOfDate = businessCard.BirthOfDate,
                                                               Email = businessCard.Email,
                                                               Gender = businessCard.Gender,
                                                               Image = Convert.ToBase64String(businessCard.Image!.EncodedImage),
                                                               Name = businessCard.Name,
                                                               Phone = businessCard.Phone,
                                                               Id = businessCard.Id,
                                                               ImageType = businessCard.Image.Type
                                                           };

            return _resultHelper.GenerateSuccessResult(businessCards);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<IQueryable<BusinessCardExport>>(ex);
        }
    }
}
