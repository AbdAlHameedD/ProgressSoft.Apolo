using ProgressSoft.Apolo.Application.Models;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application.Interfaces.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Result<Image> FindById(int id);
    }
}
