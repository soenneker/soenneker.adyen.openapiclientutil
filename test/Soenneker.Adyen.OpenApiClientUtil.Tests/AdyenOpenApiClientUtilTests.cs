using Soenneker.Adyen.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Adyen.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class AdyenOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IAdyenOpenApiClientUtil _openapiclientutil;

    public AdyenOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IAdyenOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
