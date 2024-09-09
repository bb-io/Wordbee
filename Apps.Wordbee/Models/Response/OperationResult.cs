using Newtonsoft.Json.Linq;

namespace Apps.Wordbee.Models.Response;

public class OperationResult
{
    public IEnumerable<JObject> Items { get; set; }
}