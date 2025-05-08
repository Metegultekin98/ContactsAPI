using Core.Domain.Entities.Base;

namespace Core.Domain.Entities;

public class User : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public Guid CompanyId { get; set; }
    
    public virtual Company Company { get; set; }
    public virtual ICollection<ContactInfo> ContactInfos { get; set; } = null!;

    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public User(Guid id, string firstName, string lastName)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}