using System.Runtime.Serialization;

namespace Hubbl.Core.Model
{
    [DataContract]
    public enum SourceType
    {
        [EnumMember]
        File,

        [EnumMember]
        Yandex,

        [EnumMember]
        Google,

        [EnumMember]
        VK
    }
}