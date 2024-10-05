namespace ProgressSoft.Apolo.Application;

public interface IBaseService<T> where T : class
{
    Result<IEnumerable<T>> GetAll();
    Result<T> Delete(int id);
}
