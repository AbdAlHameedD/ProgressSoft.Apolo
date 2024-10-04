namespace ProgressSoft.Apolo.Application;

public class Result<T>
{
    public OperationStatus Status { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}
