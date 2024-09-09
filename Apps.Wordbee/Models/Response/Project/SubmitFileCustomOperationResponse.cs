using Apps.Wordbee.Models.Entities;

namespace Apps.Wordbee.Models.Response.Project;

public class SubmitFileCustomOperationResponse
{
    public IEnumerable<DocumentEntity> Files { get; set; }
}