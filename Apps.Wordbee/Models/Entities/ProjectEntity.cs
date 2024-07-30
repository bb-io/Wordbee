using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Models.Entities;

public class ProjectEntity
{
    [Display("Project ID")]
    public string Id { get; set; }
    
    public string Reference { get; set; }
    
    public string Client { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    [Display("Status")]
    public string Statust { get; set; }
    
    [Display("Source language")]
    public string Src { get; set; }
    
    [Display("Manager")]
    public string Managernm { get; set; }
}