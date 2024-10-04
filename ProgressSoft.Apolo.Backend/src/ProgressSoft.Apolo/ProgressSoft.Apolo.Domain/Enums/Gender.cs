using System.Runtime.Serialization;

namespace ProgressSoft.Apolo.Domain;

public enum Gender : byte
{
    [EnumMember]
    Male = 0,

    [EnumMember]
    Female = 1
}
