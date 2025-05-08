using Core.Domain.Entities;
using Core.Persistence.Configurations.Base;
using Core.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations;

public class CompanyConfiguration : BaseConfiguration<Company, Guid>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(LengthContraints.NameMaxLength);
        builder.Property(x => x.Address).HasColumnName("Address").IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(LengthContraints.PhoneNumber);
        builder.HasMany(u => u.Users);
        builder.ToTable(TableNameConstants.COMPANY);
    }
}