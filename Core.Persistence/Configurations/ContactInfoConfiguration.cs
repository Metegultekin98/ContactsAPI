using Core.Domain.Entities;
using Core.Persistence.Configurations.Base;
using Core.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations;

public class ContactInfoConfiguration : BaseConfiguration<ContactInfo, Guid>
{
    public override void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(x => x.Type).HasColumnName("Type").IsRequired();
        builder.Property(x => x.Value).HasColumnName("Value").IsRequired();
        builder.ToTable(TableNameConstants.CONTACT_INFO);
    }
}