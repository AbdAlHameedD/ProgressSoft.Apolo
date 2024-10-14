using AutoMapper;
using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Application.Interfaces.Repositories;
using ProgressSoft.Apolo.Application.Interfaces.Services;
using ProgressSoft.Apolo.Application.Models;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        private readonly ResultHelper _resultHelper;

        public ImageService(IImageRepository imageRepository, IMapper mapper, ResultHelper resultHelper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _resultHelper = resultHelper;
        }

        public Result<ImageModel> Add(ImageModel model)
        {
            try
            {
                Image entity = _mapper.Map<Image>(model);
                Result<Image> domainResult = _imageRepository.Insert(entity);

                ImageModel insertedModel = _mapper.Map<ImageModel>(domainResult.Data);

                return _resultHelper.GenerateResultFromDifferentResultType<ImageModel, Image>(domainResult);
            }
            catch (Exception ex)
            {
                return _resultHelper.GenerateFailedResult<ImageModel>(ex);
            }
        }


        public Result<ImageModel> Delete(int id)
        {
            try
            {
                Result<Image> domainResult = _imageRepository.Delete(id);

                ImageModel deletedModel = _mapper.Map<ImageModel>(domainResult.Data);

                return _resultHelper.GenerateResultFromDifferentResultType<ImageModel, Image>(domainResult);
            }
            catch (Exception ex)
            {
                return _resultHelper.GenerateFailedResult<ImageModel>(ex);
            }
        }

        public Result<ImageModel> GetById(int id)
        {
            try
            {
                Result<Image> domainResult = _imageRepository.FindById(id);

                ImageModel model = _mapper.Map<ImageModel>(domainResult.Data);

                return _resultHelper.GenerateResultFromDifferentResultType<ImageModel, Image>(domainResult);
            }
            catch (Exception ex)
            {
                return _resultHelper.GenerateFailedResult<ImageModel>(ex);
            }
        }
    }
}
