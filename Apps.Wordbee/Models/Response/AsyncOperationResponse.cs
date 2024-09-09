namespace Apps.Wordbee.Models.Response;

public class AsyncOperationResponse
{
    public TrmResponse Trm { get; set; }
    
    public OperationResult? Result { get; set; }
    public CustomRequestData Custom { get; set; }
}