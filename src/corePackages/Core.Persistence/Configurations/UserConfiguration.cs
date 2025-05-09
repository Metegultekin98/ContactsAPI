using Core.Domain.Entities;
using Core.Persistence.Configurations.Base;
using Core.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations;

public class UserConfiguration : BaseConfiguration<User, Guid>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.CompanyId).HasColumnName("CompanyId").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(LengthContraints.NameMaxLength);
        builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(LengthContraints.NameMaxLength);
        builder.HasMany(u => u.ContactInfos);
        builder.HasMany(u => u.Reports);
        builder.ToTable(TableNameConstants.USER);
    }
}