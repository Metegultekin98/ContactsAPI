using static Core.Domain.ComplexTypes.Enums;
using Core.Domain.Entities.Base;

namespace Core.Domain.Entities;

public class Report : Entity<Guid>
{
    public DateTime RequestedDate { get; set; }
    public string RequestedFor { get; set; }
    public ReportStatu ReportStatu { get; set; }
    
    public virtual ICollection<User> Users { get; set; } = null!;
}