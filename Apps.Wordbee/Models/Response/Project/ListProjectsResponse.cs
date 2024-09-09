using Apps.Wordbee.Models.Entities;

namespace Apps.Wordbee.Models.Response.Project;

public class ListProjectsResponse
{
    public IEnumerable<ProjectEntity> Projects { get; set; }
}