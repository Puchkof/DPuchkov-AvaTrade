using AvaTrade.News.Application.Queries;
using AvaTrade.News.Domain.Entities;
using AvaTrade.News.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace AvaTrade.News.UnitTests.Queries;

public class GetLatestTopInstrumentsNewsQueryHandlerTests
{
    private readonly Mock<INewsRepository> _repositoryMock;
    private readonly Mock<ILogger<GetLatestTopInstrumentsNewsQueryHandler>> _loggerMock;
    private readonly GetLatestTopInstrumentsNewsQueryHandler _handler;

    public GetLatestTopInstrumentsNewsQueryHandlerTests()
    {
        _repositoryMock = new Mock<INewsRepository>();
        _loggerMock = new Mock<ILogger<GetLatestTopInstrumentsNewsQueryHandler>>();
        _handler = new GetLatestTopInstrumentsNewsQueryHandler(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedDtos()
    {
        // Arrange
        var limit = 5;
        var query = new GetLatestTopInstrumentsNewsQuery(limit);
        
        var newsItems = new List<NewsItem>
        {
            new(
                "Test News 1",
                "Content 1",
                "AAPL",
                DateTime.UtcNow,
                "Test Source"
            ),
            new(
                "Test News 2",
                "Content 2",
                "MSFT",
                DateTime.UtcNow,
                "Test Source"
            )
        };

        _repositoryMock
            .Setup(r => r.GetLatestForTopInstrumentsAsync(5, limit))
            .ReturnsAsync(newsItems);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        var dtos = result.ToList();
        Assert.Equal(newsItems.Count, dtos.Count);
        
        for (var i = 0; i < newsItems.Count; i++)
        {
            Assert.Equal(newsItems[i].Id, dtos[i].Id);
            Assert.Equal(newsItems[i].Title, dtos[i].Title);
            Assert.Equal(newsItems[i].Content, dtos[i].Content);
            Assert.Equal(newsItems[i].InstrumentName, dtos[i].InstrumentName);
            Assert.Equal(newsItems[i].PublishedAt, dtos[i].PublishedAt);
            Assert.Equal(newsItems[i].Source, dtos[i].Source);
        }

        _repositoryMock.Verify(
            r => r.GetLatestForTopInstrumentsAsync(5, limit), 
            Times.Once);
    }
} 