namespace Apps.Wordbee.Models.Entities;

public class UserEntity
{
    public string PersonId { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }

    public string DisplayName => $"{FirstName} {LastName}";
}