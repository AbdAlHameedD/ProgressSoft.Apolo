using AutoMapper;
using CsvHelper;
using ProgressSoft.Apolo.Application;
using ProgressSoft.Apolo.Application.DTOs;
using ProgressSoft.Apolo.Application.Helpers;
using ProgressSoft.Apolo.Application.Interfaces.Services;
using ProgressSoft.Apolo.Application.Models;
using ProgressSoft.Apolo.Domain;
using System.Data;
using System.Globalization;
using System.Xml;

namespace ProgressSoft.Apolo.Infrastructure;

public class BusinessCardService : IBusinessCardService
{
    private readonly IBusinessCardRepository _businessCardRepository;
    private readonly IMapper _mapper;
    private readonly ResultHelper _resultHelper;
    private readonly IImageService _imageService;

    public BusinessCardService(IBusinessCardRepository businessCardRepository, IMapper mapper, ResultHelper resultHelper, IImageService imageService)
    {
        _businessCardRepository = businessCardRepository;
        _mapper = mapper;
        _resultHelper = resultHelper;
        _imageService = imageService;
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

            return _resultHelper.GenerateResultFromDifferentResultType<BusinessCardModel, BusinessCard>(domainResult);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<BusinessCardModel>(ex);
        }
    }

    public MemoryStream ExportCSV(BusinessCardFilter filter)
    {
        MemoryStream memoryStream = new MemoryStream();

        try
        {
            CsvWriter csvWriter = new CsvWriter(new StreamWriter(memoryStream), new CultureInfo("en"));

            IEnumerable<ExportBusinessCard>? cards = _businessCardRepository.GetForExport(filter).Data;
            if (cards is not null)
            {
                csvWriter.Context.RegisterClassMap<ExportBusinessCardMap>();
                csvWriter.WriteRecords(cards);
            }
            csvWriter.Flush();
            memoryStream.Position = 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }

        return memoryStream;
    }

    public MemoryStream ExportXML(BusinessCardFilter filter)
    {
        MemoryStream memoryStream = new MemoryStream();

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement rootElement = xmlDocument.CreateElement("Apolo");
            xmlDocument.AppendChild(rootElement);

            IEnumerable<ExportBusinessCard>? cards = _businessCardRepository.GetForExport(filter).Data;
            if (cards is not null)
            {
                foreach (ExportBusinessCard card in cards)
                {
                    XmlElement cardElement = xmlDocument.CreateElement("BusinessCard");

                    cardElement.SetAttribute("Id", card.Id.ToString());
                    cardElement.SetAttribute("Name", card.Name);
                    cardElement.SetAttribute("Gender", card.Gender.ToString());
                    cardElement.SetAttribute("BirthOfDate", card.BirthOfDate.ToString("yyyy-MM-dd"));
                    cardElement.SetAttribute("Email", card.Email);
                    cardElement.SetAttribute("Phone", card.Phone);
                    cardElement.SetAttribute("Address", card.Address);
                    cardElement.SetAttribute("Image", card.Image);
                    cardElement.SetAttribute("ImageType", card.ImageType);

                    rootElement.AppendChild(cardElement);
                }
            }

            xmlDocument.Save(memoryStream);
            memoryStream.Position = 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }

        return memoryStream;
    }

    public Result<IEnumerable<BusinessCardModel>> AddBulk(IEnumerable<ImportBusinessCard> businessCards)
    {
        try
        {
            List<BusinessCard> cards = new List<BusinessCard>(businessCards.Count());
            foreach (ImportBusinessCard businessCard in businessCards)
            {
                BusinessCardModel card = _mapper.Map<BusinessCardModel>(businessCard);
                if (card.ImageId == -1)
                {
                    Result<ImageModel> imageDomainResult = _imageService.Add(new ImageModel { EncodedImage = businessCard.Image!, Type = businessCard.ImageType! });
                    card.ImageId = imageDomainResult.Data.Id;
                }

                cards.Add(_mapper.Map<BusinessCard>(card));
            }

            Result<IEnumerable<BusinessCard>> domainResult = _businessCardRepository.InsertBulk(cards);

            return _resultHelper.GenerateResultFromDifferentResultType<IEnumerable<BusinessCardModel>, IEnumerable<BusinessCard>>(domainResult);
        }
        catch (Exception ex)
        {
            return _resultHelper.GenerateFailedResult<IEnumerable<BusinessCardModel>>(ex);
        }
    }
}

