using Core.Domain.Entities.Base;

namespace Core.Domain.Entities;

public class Company : Entity<Guid>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    
    public virtual ICollection<User> Users { get; set; } = null!;

    public Company()
    {
        Name = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }

    public Company(Guid id, string name, string address, string phoneNumber)
        : base(id)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}