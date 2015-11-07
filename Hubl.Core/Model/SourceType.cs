using System.Runtime.Serialization;

namespace Hubl.Core.Model
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