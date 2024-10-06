namespace ProgressSoft.Apolo.Application;

public interface IBaseService<T> where T : class
{
    Result<T> Delete(int id);
}
