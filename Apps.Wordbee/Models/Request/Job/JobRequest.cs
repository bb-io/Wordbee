using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Models.Request.Job;

public class JobRequest
{
    [Display("Job ID")]
    [DataSource(typeof(JobDataSourceHandler))]
    public string JobId { get; set; }
}