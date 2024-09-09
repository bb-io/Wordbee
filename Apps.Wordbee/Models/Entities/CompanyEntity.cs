using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Models.Entities;

public class CompanyEntity
{
    [Display("Company ID")]
    public string CompanyId { get; set; }
    
    public string Name { get; set; }
}