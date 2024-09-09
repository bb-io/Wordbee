using Newtonsoft.Json;

namespace Apps.Wordbee.Models.Response;

public class OperationResultResponse<T>
{
    [JsonProperty("v")]
    public T Result { get; set; }
}