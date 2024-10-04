using System.Runtime.Serialization;

namespace ProgressSoft.Apolo.Application;

public enum OperationStatus : short
{
    [EnumMember]
    Failed = 0,

    [EnumMember]
    Success = 1,

    [EnumMember]
    PartialSuccess = 2,
}
