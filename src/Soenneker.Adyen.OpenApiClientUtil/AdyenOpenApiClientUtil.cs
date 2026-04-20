using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Adyen.HttpClients.Abstract;
using Soenneker.Adyen.OpenApiClientUtil.Abstract;
using Soenneker.Adyen.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Adyen.OpenApiClientUtil;

///<inheritdoc cref="IAdyenOpenApiClientUtil"/>
public sealed class AdyenOpenApiClientUtil : IAdyenOpenApiClientUtil
{
    private readonly AsyncSingleton<AdyenOpenApiClient> _client;

    public AdyenOpenApiClientUtil(IAdyenOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<AdyenOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Adyen:ApiKey");
            string authHeaderValueTemplate = configuration["Adyen:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new AdyenOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<AdyenOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
