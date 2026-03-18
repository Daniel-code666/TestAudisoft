using System.Text.Json.Serialization;

namespace TestAudisoft.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DbActions
    {
        Created = 1,
        NotCreated = 2,
        NotFound = 3,
        NotUpdated = 4,
        Updated = 5,
        Deleted = 6,
        NotDeleted = 7,
    }
}
