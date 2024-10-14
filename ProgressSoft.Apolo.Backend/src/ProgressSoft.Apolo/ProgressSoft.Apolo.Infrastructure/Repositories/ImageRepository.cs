using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Application.Interfaces.Repositories;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        private readonly ApoloDbContext _apoloDbContext;
        private readonly ResultHelper _resultHelper;

        public ImageRepository(ApoloDbContext apoloDbContext, ResultHelper resultHelper) : base(apoloDbContext, resultHelper)
        {
            _apoloDbContext = apoloDbContext;
            _resultHelper = resultHelper;
        }

        /// <summary>
        /// Delete image from database
        /// </summary>
        /// <param name="id">Image identity</param>
        /// <returns>Deleted image</returns>
        public Result<Image> Delete(int id)
        {
            try
            {
                Image? entity = (from image in _apoloDbContext.Images
                                 where image.Id == id
                                 select image).FirstOrDefault();

                if (entity is not null)
                {
                    _apoloDbContext.Remove(entity);

                    bool isDeleted = _apoloDbContext.SaveChangesAsync().Result > 0;
                }

                return _resultHelper.GenerateSuccessResult<Image>(entity);
            }
            catch (Exception ex)
            {
                return _resultHelper.GenerateFailedResult<Image>(ex);
            }
        }

        /// <summary>
        /// Get image that match target id
        /// </summary>
        /// <param name="id">Image identity</param>
        /// <returns>Matched entity</returns>
        public Result<Image> FindById(int id)
        {
            try
            {
                Image? entity = (from image in _apoloDbContext.Images
                                        where image.Id == id
                                        select image).First();

                return _resultHelper.GenerateSuccessResult(entity);
            }
            catch (Exception ex)
            {
                return _resultHelper.GenerateFailedResult<Image>(ex);
            }
        }
    }
}
