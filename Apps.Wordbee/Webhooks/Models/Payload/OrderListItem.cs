using Apps.Wordbee.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Wordbee.Webhooks.Models.Payload
{
    public class OrderListItem
    {
        public string? reference { get; set; }
        public string status { get; set; }
        public string? statust { get; set; }
        public string? id { get; set; }
        public string? orderId { get; set; }
        public string? companyName { get; set; }
        public string? companyId { get; set; }
        public string? personName { get; set; }
        public string? personId { get; set; }
        public string? srct { get; set; }
        public string? src { get; set; }
        public string? trg { get; set; }
        public string? trgt { get; set; }
        public DateTime? created { get; set; }
        public DateTime? dtreceived { get; set; }
        public DateTime? deadline { get; set; }
        public string? projectId { get; set; }
        public string? projectManagerId { get; set; }
        public string? projectResourceId { get; set; }
        public string? projectStatust { get; set; }
    }

    public class SingleOrderStatusMemory
    {
        public DateTime LastCheckedUtc { get; set; }
        public string? LastKnownStatus { get; set; }
        public bool Notified { get; set; }
    }

    public class OrderStatusReached
    {
        [Display("Order ID")]
        public string OrderId { get; set; }

        [Display("Reference")]
        public string? Reference { get; set; }

        [Display("Status")]
        public string Status { get; set; }

        [Display("Status Title")]
        public string? StatusTitle { get; set; }

        [Display("Old Status")]
        public string? OldStatus { get; set; }

        [Display("Internal ID")]
        public string? InternalId { get; set; }

        [Display("Company name")]
        public string? CompanyName { get; set; }

        [Display("Company ID")]
        public string? CompanyId { get; set; }

        [Display("Person name")]
        public string? PersonName { get; set; }

        [Display("Person ID")]
        public string? PersonId { get; set; }

        [Display("Source language title")]
        public string? SourceLangTitle { get; set; }

        [Display("Source language")]
        public string? SourceLang { get; set; }

        [Display("Target language")]
        public string? TargetLang { get; set; }

        [Display("Target language title")]
        public string? TargetLangTitle { get; set; }

        [Display("Created")]
        public DateTime? Created { get; set; }

        [Display("Date received")]
        public DateTime? DateReceived { get; set; }

        [Display("Deadline")]
        public DateTime? Deadline { get; set; }

        [Display("Project ID")]
        public string? ProjectId { get; set; }

        [Display("Project manager ID")]
        public string? ProjectManagerId { get; set; }

        [Display("Project resource ID")]
        public string? ProjectResourceId { get; set; }

        [Display("Project status title")]
        public string? ProjectStatusTitle { get; set; }
    }

    public class OrderStatusReachedResponse
    {
        public OrderStatusReached Order { get; set; } = new();
    }

    public class SearchOrdersPollingRequest
    {
        [StaticDataSource(typeof(OrderStatusStaticHandler))]
        public string Status { get; set; }
    }
}
