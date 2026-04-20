using Soenneker.Adyen.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Adyen.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class AdyenOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IAdyenOpenApiClientUtil _openapiclientutil;

    public AdyenOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IAdyenOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
