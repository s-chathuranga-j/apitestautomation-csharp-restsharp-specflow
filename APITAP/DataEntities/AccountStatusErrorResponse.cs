using Newtonsoft.Json;

namespace APITAP.DataEntities;

public class AccountStatusErrorResponse
{
    [JsonProperty("type")]
    public string Type { get; set; }
    [JsonProperty("code")]
    public string Code { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
    [JsonProperty("customerFacingMessage")]
    public string CustomerFacingMessage { get; set; }
    [JsonProperty("actionCode")]
    public string ActionCode { get; set; }
    [JsonProperty("fieldReference")]
    public string FieldReference { get; set; }
}