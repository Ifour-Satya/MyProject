using System.Text.Json.Serialization;

namespace Customer.Microservice.API.Request
{
    public class AddColumnRequest
    {
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        public bool AllowNull { get; set; }
        [JsonIgnore]
        public string? DefaultValue { get; set; }
    }
}
