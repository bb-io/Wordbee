using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Models.Entities;

public class JobEntity
{
    [Display("Job ID")]
    public string Id { get; set; }
    public string Reference { get; set; }
    
    [Display("Status")]
    public string Statust { get; set; }
    
    [Display("Source language")]
    public string Src { get; set; }
    
    [Display("Target language")]
    public string Trg { get; set; }
    
    public DateTime Deadline { get; set; }
}