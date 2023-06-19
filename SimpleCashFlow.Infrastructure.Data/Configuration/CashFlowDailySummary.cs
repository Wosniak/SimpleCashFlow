using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCashFlow.Domain.ValueObjects;

namespace SimpleCashFlow.Infrastructure.Data.Configuration
{
    internal class CashFlowDailySummaryConfiguration : IEntityTypeConfiguration<CashFlowDailySummary>
    {
        public void Configure(EntityTypeBuilder<CashFlowDailySummary> builder)
        {
            builder.HasKey(ds => ds.Id);
            builder.Property(ds => ds.MovementItems).HasColumnType("jsonb");
            builder.Property(ds => ds.Date).HasColumnType("date");
            builder.Property(ds => ds.TotalCreditAmount);
            builder.Property(ds => ds.TotalDebitAmount);
            builder.Property(ds => ds.TotalAmount);

            builder.HasIndex(ds => ds.Date).IsUnique();

        }
    }
}
