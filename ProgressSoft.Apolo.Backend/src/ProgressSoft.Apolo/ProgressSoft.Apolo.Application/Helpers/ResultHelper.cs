namespace ProgressSoft.Apolo.Application;

public class ResultHelper
{
    public static Result<T> GenerateFailedResult<T>(Exception ex)
    {
        return new Result<T>
        {
            Status = OperationStatus.Failed,
            Message = ex.Message
        };
    }
}
