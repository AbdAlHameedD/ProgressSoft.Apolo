using System.Net.NetworkInformation;
using AutoMapper;

namespace ProgressSoft.Apolo.Application;

public class ResultHelper
{
    private readonly IMapper _mapper;

    public ResultHelper(IMapper mapper) 
    {
        _mapper = mapper;
    }

    public Result<T> GenerateFailedResult<T>(Exception ex)
    {
        return new Result<T>
        {
            Status = OperationStatus.Failed,
            Message = ex.Message
        };
    }

    public Result<T> GenerateSuccessResult<T>(T data)
    {
        return new Result<T>
        {
            Status = OperationStatus.Success,
            Data = data
        };
    }

    public Result<T> GenerateResultFromDifferentResultType<T, U>(Result<U> result)
    {
        try
        {
            return new Result<T>
            {
                Status = result.Status,
                Data = _mapper.Map<T>(result.Data),
                Message = result.Message
            };
        }
        catch (Exception ex)
        {
            return new Result<T>
            {
                Status = OperationStatus.Failed,
                Message = ex.Message
            };
        }
    }
}
