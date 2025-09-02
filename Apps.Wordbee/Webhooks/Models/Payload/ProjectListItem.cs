using Apps.Wordbee.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Wordbee.Webhooks.Models.Payload
{
    public class ProjectListItem
    {
        public string? reference { get; set; }
        public string status { get; set; }
        public string? statust { get; set; }
        public string? id { get; set; }
        public int? iscodyt { get; set; }
        public string? iscodytt { get; set; }
        public string? client { get; set; }
        public DateTime? deadline { get; set; }
        public string? comments { get; set; }
        public string? srct { get; set; }
        public string? src { get; set; }
        public DateTime? dtreceived { get; set; }
        public DateTime? dtinprogress { get; set; }
        public DateTime? dtcompletion { get; set; }
        public DateTime? dtarchival { get; set; }
        public string? instructions { get; set; }
        public string? managernm { get; set; }
    }

    public class SingleProjectStatusMemory
    {
        public DateTime LastCheckedUtc { get; set; }
        public string? LastKnownStatus { get; set; }
        public bool Notified { get; set; }
    }

    public class ProjectStatusReached
    {
        [Display("Project ID")]
        public string ProjectId { get; set; }

        [Display("Reference")]
        public string? Reference { get; set; }

        [Display("Status")]
        public string Status { get; set; }

        [Display("Status Title")]
        public string? StatusTitle { get; set; }

        [Display("Old Status")]
        public string? OldStatus { get; set; }

        [Display("Client")]
        public string? Client { get; set; }

        [Display("Deadline")]
        public DateTime? Deadline { get; set; }

        [Display("Source language title")]
        public string? SourceLangTitle { get; set; }

        [Display("Source language")]
        public string? SourceLang { get; set; }

        [Display("Date received")]
        public DateTime? DateReceived { get; set; }

        [Display("Date in progress")]
        public DateTime? DateInProgress { get; set; }

        [Display("Date completed")]
        public DateTime? DateCompleted { get; set; }

        [Display("Date archived")]
        public DateTime? DateArchived { get; set; }
    }

    public class ProjectStatusReachedResponse
    {
        public ProjectStatusReached Project { get; set; } = new();
    }

    public class SearchProjectsPollingRequest
    {
        [StaticDataSource(typeof(ProjectStatusStaticHandler))]
        public string Status { get; set; }
    }
}
