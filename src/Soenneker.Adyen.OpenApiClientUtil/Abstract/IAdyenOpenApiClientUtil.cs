using Soenneker.Adyen.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Adyen.OpenApiClientUtil.Abstract;

/// <summary>
/// Creates and caches a configured <see cref="AdyenOpenApiClient"/>.
/// </summary>
public interface IAdyenOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel initial client creation.</param>
    /// <returns>The cached generated client.</returns>
    ValueTask<AdyenOpenApiClient> Get(CancellationToken cancellationToken = default);
}
