using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Models.Request.Project;

public class SubmitNewProjectFileInput
{
    public DateTime? Deadline { get; set; }
    
    [Display("Disable machine translation")]
    public bool? DisableMt { get; set; }
    
    public string? Reference { get; set; }
    
    public IEnumerable<string>? Comments { get; set; }
}