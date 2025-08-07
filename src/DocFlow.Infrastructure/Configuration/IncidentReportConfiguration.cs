using DocFlow.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocFlow.Infrastructure.Configuration;

public class IncidentReportConfiguration: IEntityTypeConfiguration<IncidentReport>
{
    public void Configure(EntityTypeBuilder<IncidentReport> builder)
    {
        builder.ToTable("IncidentReports","dd");
        builder.HasKey(ir => ir.AmbientStateId);
        builder.Property(ir => ir.AmbientStateId)
            .ValueGeneratedNever()
            .IsRequired();
    }
}