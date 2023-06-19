using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.Entities.Base;
using SimpleCashFlow.Domain.ValueObjects;

namespace SimpleCashFlow.Infrastructure.Data.Context
{
    public class SimpleCashFlowContext : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _config;
        private readonly IPublisher _publisher;

        public SimpleCashFlowContext(IConfiguration config, IPublisher publisher)
        {
            _config = config;
            _publisher = publisher;
        }


        public DbSet<Movement> Movements { get; set; }

        public DbSet<CashFlowDailySummary> Summaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseNpgsql(_config.GetConnectionString("db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleCashFlowContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
                .Select(e => e.Entity)
                .Where(e => e.DomainEvent.Any())
                .SelectMany(e => e.DomainEvent)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
