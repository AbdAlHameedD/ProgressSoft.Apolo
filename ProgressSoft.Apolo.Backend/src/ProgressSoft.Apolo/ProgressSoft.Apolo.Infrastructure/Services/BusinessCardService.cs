using AutoMapper;
using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure;

public class BusinessCardService : IBusinessCardService
{
    private readonly IBusinessCardRepository _businessCardRepository;
    private readonly IMapper _mapper;
    private readonly ResultHelper _resultHelper;

    public BusinessCardService(IBusinessCardRepository businessCardRepository, IMapper mapper, ResultHelper resultHelper) 
    {
        _businessCardRepository = businessCardRepository;
        _mapper = mapper;
        _resultHelper = resultHelper;
    }

    public Result<BusinessCardModel> Add(AddBusinessCardCommand command)
    {
        try 
        {
            BusinessCard entity = _mapper.Map<BusinessCard>(command);
            Result<BusinessCard> domainResult = _businessCardRepository.Insert(entity);

            BusinessCardModel insertedModel = _mapper.Map<BusinessCardModel>(domainResult.Data);
            
            return _resultHelper.GenerateResultFromDifferentResultType<BusinessCardModel, BusinessCard>(domainResult);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCardModel>(ex);
        }
    }

    public Result<BusinessCardModel> Delete(int id)
    {
        try
        {
            Result<BusinessCard> domainResult = _businessCardRepository.Delete(id);

            BusinessCardModel deletedModel = _mapper.Map<BusinessCardModel>(domainResult.Data);

            return _resultHelper.GenerateResultFromDifferentResultType<BusinessCardModel, BusinessCard>(domainResult);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCardModel>(ex);
        }
    }

    public Result<BusinessCardModel> Edit(BusinessCardModel model)
    {
        try
        {
            BusinessCard entity = _mapper.Map<BusinessCard>(model);
            Result<BusinessCard> domainResult = _businessCardRepository.Update(entity);

            BusinessCardModel editedModel = _mapper.Map<BusinessCardModel>(domainResult.Data);

            return _resultHelper.GenerateResultFromDifferentResultType<BusinessCardModel, BusinessCard>(domainResult);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCardModel>(ex);
        }
    }

    public Result<IEnumerable<BusinessCardModel>> GetAll(BusinessCardFilter filter)
    {
        try
        {
            Result<IQueryable<BusinessCard>> domainResult = _businessCardRepository.Get(filter);
            
            IEnumerable<BusinessCardModel>? businessCardModels = domainResult.Data?.Select(b => _mapper.Map<BusinessCardModel>(b));
            
            return _resultHelper.GenerateResultFromDifferentResultType<IEnumerable<BusinessCardModel>, IQueryable<BusinessCard>>(domainResult);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<IEnumerable<BusinessCardModel>>(ex);
        }
    }

    public Result<BusinessCardModel> GetById(int id)
    {
        try
        {
            Result<BusinessCard> domainResult = _businessCardRepository.FindById(id);

            BusinessCardModel model = _mapper.Map<BusinessCardModel>(domainResult.Data);

            return _resultHelper.GenerateResultFromDifferentResultType<BusinessCardModel, BusinessCard>(domainResult);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCardModel>(ex);
        }
    }
}
