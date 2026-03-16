using System.Text.Json.Serialization;

namespace TestAudisoft.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        
    }
}
