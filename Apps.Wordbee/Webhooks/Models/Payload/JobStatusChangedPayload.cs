using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Webhooks.Models.Payload;

public class JobStatusChangedPayload : WordbeeWebhookPayload
{
    [Display("Job ID")]
    public string JobId { get; set; }

    [Display("Job reference")]
    public string JobReference { get; set; }

    [Display("Job URL")]
    public string Url { get; set; }

    [Display("Job deadline")]
    public DateTime JobDeadlineX { get; set; }

    [Display("Job status")]
    public string JobStatus { get; set; }

    [Display("Job status code")]
    public string JobStatusCode { get; set; }

    [Display("Job task")]
    public string JobTask { get; set; }

    [Display("Job task code")]
    public string JobTaskCode { get; set; }

    [Display("Job source language")]
    public string JobSourceLanguage { get; set; }

    [Display("Job target language")]
    public string JobTargetLanguage { get; set; }

    [Display("Job source language name")]
    public string JobSourceLanguageName { get; set; }

    [Display("Job target language name")]
    public string JobTargetLanguageName { get; set; }
}