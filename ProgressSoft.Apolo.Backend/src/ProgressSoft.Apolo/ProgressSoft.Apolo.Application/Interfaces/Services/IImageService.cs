using ProgressSoft.Apolo.Application.Models;

namespace ProgressSoft.Apolo.Application.Interfaces.Services
{
    public interface IImageService : IBaseService<ImageModel>
    {
        Result<ImageModel> Add(ImageModel model);
        Result<ImageModel> GetById(int id);
    }
}
