using Newtonsoft.Json;

namespace Apps.Wordbee.Models.Entities;

public class LanguageEntity
{
    [JsonProperty("v")]
    public string Code { get; set; }
    
    [JsonProperty("t")]
    public string Name { get; set; }
    
    [JsonProperty("src")]
    public bool IsSourceLanguage { get; set; }
}