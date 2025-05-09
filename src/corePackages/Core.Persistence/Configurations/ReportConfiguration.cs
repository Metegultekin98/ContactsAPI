using Core.Domain.Entities;
using Core.Persistence.Configurations.Base;
using Core.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations;

public class ReportConfiguration : BaseConfiguration<Report, Guid>
{
    public override void Configure(EntityTypeBuilder<Report> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.RequestedDate).HasColumnName("RequestedDate").IsRequired();
        builder.Property(x => x.RequestedFor).HasColumnName("RequestedFor").IsRequired();
        builder.Property(x => x.ReportStatu).HasColumnName("ReportStatu").IsRequired();
        builder.HasMany(r => r.Users)
            .WithMany(u => u.Reports)
            .UsingEntity(j => j.ToTable(TableNameConstants.USER_REPORT));
        builder.ToTable(TableNameConstants.REPORT);
    }
}