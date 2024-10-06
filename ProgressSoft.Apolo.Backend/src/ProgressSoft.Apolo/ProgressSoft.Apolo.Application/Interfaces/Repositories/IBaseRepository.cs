namespace ProgressSoft.Apolo.Application;

public interface IBaseRepository<T> where T : class
{
    Result<T> Insert(T entity);
    Result<T> Update(T entity);
    Result<T> Delete(int id);
}
