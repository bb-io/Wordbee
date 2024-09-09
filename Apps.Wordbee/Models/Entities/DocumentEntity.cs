using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Models.Entities;

public class DocumentEntity
{
    [Display("Document ID")]
    public string Did { get; set; }
    
    public string Name { get; set; }
    
    public bool Exists { get; set; }
    
    [Display("Segments count")]
    public long Segments { get; set; }
}