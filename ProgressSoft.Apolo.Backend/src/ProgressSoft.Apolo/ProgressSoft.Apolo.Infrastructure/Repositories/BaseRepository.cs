using Microsoft.Identity.Client;
using ProgressSoft.Apolo.Application;

namespace ProgressSoft.Apolo.Infrastructure;

public abstract class BaseRepository<T> where T : class
{
    private readonly ApoloDbContext _apoloDbContext;

    public BaseRepository(ApoloDbContext apoloDbContext)
    {
        _apoloDbContext = apoloDbContext;
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

            return new Result<T>
            {
                Status = OperationStatus.Success,
                Data = entity
            };
        }
        catch (Exception ex)
        {
            return new Result<T>
            {
                Status = OperationStatus.Failed,
                Message = ex.Message
            };
        }
    }
}
