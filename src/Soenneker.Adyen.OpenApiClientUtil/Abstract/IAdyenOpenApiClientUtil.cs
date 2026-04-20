using Soenneker.Adyen.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Adyen.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IAdyenOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<AdyenOpenApiClient> Get(CancellationToken cancellationToken = default);
}
