using Microsoft.Identity.Client;
using ProgressSoft.Apolo.Application;

namespace ProgressSoft.Apolo.Infrastructure;

public abstract class BaseRepository<T> where T : class
{
    private readonly ApoloDbContext _apoloDbContext;
    private readonly ResultHelper _resultHelper;

    public BaseRepository(ApoloDbContext apoloDbContext, ResultHelper resultHelper)
    {
        _apoloDbContext = apoloDbContext;
        _resultHelper = resultHelper;
    }

    /// <summary>
    /// Update database generic entity
    /// </summary>
    /// <param name="entity">Generic database entity to update</param>
    /// <returns>Updated entity</returns>
    public Result<T> Update(T entity)
    {
        try 
        {
            _apoloDbContext.Update(entity);

            return _resultHelper.GenerateSuccessResult(entity);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<T>(ex);
        }
    }
}
