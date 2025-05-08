using Core.Domain.Entities.Base;

namespace Core.Domain.Entities;

public class ContactInfo : Entity<Guid>
{
    public string Type { get; set; }
    public string Value { get; set; }
    
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; }
}