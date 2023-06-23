using Moq;
using SimpleCashFlow.Application.Movements.Queries;
using SimpleCashFlow.Application.Summary.Queries;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.ValueObjects;

namespace SimpleCashFlow.Tests.Summaries.Queries
{
    public class GetDailySummaryByDateTests
    {
        private readonly Mock<ICashFlowDailySummaryRepository> _summaryRepositoryMock;


        public GetDailySummaryByDateTests()
        {
            _summaryRepositoryMock = new();
        }


        [Fact]
        public async Task Query_Shoud_ReturnResultWithNullValue()
        {

            //Arrange
            CashFlowDailySummary? summary = null;

            var dateReference = DateTime.Now;

            _summaryRepositoryMock.Setup(
                sr => sr.GetByDateAsync(
                    It.IsAny<DateOnly>(),
                    default
                    )
                ).ReturnsAsync(summary!);

            var query = new GetDailySummaryByDateQuery(DateOnly.FromDateTime(dateReference));

            var handler = new GetDailySummaryByDateQueryHandler(_summaryRepositoryMock.Object);

            //Act

            var result = await handler.Handle(query, default);

            //Assert

            Assert.True(result.IsSuccess);
            Assert.Null(result.Value);
        }


        [Fact]
        public async Task Query_Shoud_ReturnResultWithValue()
        {

            //Arrange
            var dateReference = DateTime.Now;
            CashFlowDailySummary summary = new CashFlowDailySummary(Guid.NewGuid(), DateOnly.FromDateTime(dateReference), 100, -200, -100,
                new List<CashFlowDailySummary.MovementItem>() {
                    new CashFlowDailySummary.MovementItem(Guid.NewGuid(), dateReference, 100, "First entry"),
                    new CashFlowDailySummary.MovementItem(Guid.NewGuid(), dateReference, -200, "First entry")
                });


            _summaryRepositoryMock.Setup(
                sr => sr.GetByDateAsync(
                    It.IsAny<DateOnly>(),
                    default
                    )
                ).ReturnsAsync(summary);

            var query = new GetDailySummaryByDateQuery(DateOnly.FromDateTime(dateReference));

            var handler = new GetDailySummaryByDateQueryHandler(_summaryRepositoryMock.Object);

            //Act

            var result = await handler.Handle(query, default);

            //Assert

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(summary.Id, result.Value.Id);
        }
    }
}
