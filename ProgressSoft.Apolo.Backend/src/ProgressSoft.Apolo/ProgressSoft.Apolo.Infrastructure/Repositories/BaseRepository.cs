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

            bool isUpdated = _apoloDbContext.SaveChangesAsync().Result > 0;

            return _resultHelper.GenerateSuccessResult(entity);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<T>(ex);
        }
    }

    /// <summary>
    /// Insert new entity in the database
    /// </summary>
    /// <param name="entity">Represent entity to insert</param>
    /// <returns>Inserted entity</returns>
    public Result<T> Insert(T entity)
    {
        try
        {
            _apoloDbContext.Add(entity);

            bool isInserted = _apoloDbContext.SaveChangesAsync().Result > 0;

            return _resultHelper.GenerateSuccessResult(entity);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<T>(ex);
        }
    }
}
