namespace Apps.Wordbee.Models.Response;

public class PaginationResponse<T>
{
    public int Total { get; set; }
    public IEnumerable<T> Rows { get; set; }
    public IEnumerable<T> Items { get; set; }
}