using System.Text.Json;

using DocFlow.Application.Persistence.Engine;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.ApplicationTests.Persistence.Engine;
public class DocumentFactoryTests
{
    private record TestData(string? Name);

    private class TestDocument : DocumentData<TestData>
    {
    }

    private class DummySequenceSource : ISequenceSource
    {
        private int _current = 1;

        public Task<Guid> GetNextGuidAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Guid.CreateVersion7());
        }

        public Task<int> GetNextSequenceAsync(CancellationToken cancellationToken) => Task.FromResult(_current++);
    }

    private readonly Fixture _fixture = new();


    private readonly ISequenceSource _sequenceSource = new DummySequenceSource();
    [Fact]
    public async Task CreateFromJson_ShouldCreateTestDocumentWithCorrectData()
    {
        // Arrange

        var factory = new DocumentFactory(_sequenceSource);
        var typeOfData = typeof(TestData);
        var station = CreateStation(typeOfData);
        var testName = "TestName";
        var json = JsonSerializer.SerializeToNode(new { Name = testName })!.AsObject();

        // Act
        var result = await factory.CreateFromJson(station, json, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        var doc = Assert.IsType<DocumentData<TestData>>(result.Value);
        Assert.Equal(testName, doc.Data.Name);
        Assert.True(doc.Id.Value > 0);
    }

    private Station CreateStation(Type typeOfData)
    {
        return _fixture.Build<Station>()
                    .With(
                      e => e.Track,
                      _fixture.Build<Track>()
                        .With(
                          t => t.Formular,
                          _fixture.Build<Formular>()
                            .With(f => f.DocumentData, typeOfData)
                            .Create())
                      .Create())
                    .Create();
    }

    [Fact]
    public async Task CreateFromJson_ShouldFailForInvalidJson()
    {
        // Arrange

        var factory = new DocumentFactory(_sequenceSource);
        var station = CreateStation(typeof(TestData));
        var invalidJson = JsonSerializer.SerializeToNode(new { Invalid = "Field" })!.AsObject();

        // Act
        var result = await factory.CreateFromJson(station, invalidJson, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess); // Deserialization will succeed, but Data.Name will be null
        var doc = Assert.IsType<DocumentData<TestData>>(result.Value);
        Assert.Null(doc.Data.Name);
    }

}
