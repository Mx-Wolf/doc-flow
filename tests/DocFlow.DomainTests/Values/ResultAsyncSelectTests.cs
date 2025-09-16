using DocFlow.Domain.Values;

namespace DocFlow.DomainTests.Values;
public class ResultAsyncSelectTests
{
    static Task<Result<int,Exception>> Method1Async() => Task.FromResult(Result<int,Exception>.Success(1));
    static Task<Result<int, Exception>> Method2Async() => Task.FromResult(Result<int, Exception>.Success(2));
    static Task<Result<int, Exception>> Method3Async(int a, int b) => Task.FromResult(Result<int, Exception>.Success(a + b));
    [Fact]
    public async Task ExampleOfLink()
    {

        var result = await
            (from x in Method1Async()
             from y in Method2Async()
             select Method3Async(x, y));

        Assert.True(result.IsSuccess);
    }
}
