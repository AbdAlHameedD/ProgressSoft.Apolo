namespace ProgressSoft.Apolo.Application;

public class AddBusinessCardCommandHandler : ICommandHandler<AddBusinessCardCommand, Result<BusinessCardModel>>
{
    private readonly IBusinessCardService _businessCardService;

    public AddBusinessCardCommandHandler(IBusinessCardService businessCardService)
    {
        _businessCardService = businessCardService;
    }

    public Result<BusinessCardModel> Handle(AddBusinessCardCommand command)
    {
        return _businessCardService.Add(command);
    }
}
