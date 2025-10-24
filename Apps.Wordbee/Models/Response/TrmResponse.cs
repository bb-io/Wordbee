namespace Apps.Wordbee.Models.Response;

public class TrmResponse
{
    public string RequestId { get; set; }
    public string Status { get; set; }
    public string StatusInfo { get; set; }

    public string? StatusText { get; set; }
}