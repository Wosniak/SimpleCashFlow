using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCashFlow.Domain.Entities;

namespace SimpleCashFlow.Infrastructure.Data.Configuration
{
    internal class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.HasKey(mov => mov.Id);

            builder.Property(mov => mov.Id).HasConversion(
                id => id.Value,
                value => new MovementId(value));

            builder.Property(mov => mov.Amount).IsRequired();
            builder.Property(mov => mov.Date).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(mov => mov.CreatedAt).HasColumnType("timestamp without time zone");
            builder.Property(mov => mov.ChangedAt).HasColumnType("timestamp without time zone");
            builder.Property(mov => mov.Classification).HasMaxLength(150).IsRequired();

            builder.HasIndex(mov => mov.Date);

            builder.HasQueryFilter(m => !m.Deleted);

            builder.Ignore(mov => mov.DomainEvent);
        }
    }
}
