[![](https://img.shields.io/nuget/v/soenneker.adyen.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.adyen.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.adyen.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.adyen.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.adyen.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.adyen.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.adyen.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.adyen.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Adyen.OpenApiClientUtil

Creates and caches a configured `AdyenOpenApiClient` for dependency-injected applications.

## Installation

```bash
dotnet add package Soenneker.Adyen.OpenApiClientUtil
```

## Configuration

```json
{
  "Adyen": {
    "ApiKey": "your-api-key",
    "ClientBaseUrl": "https://your-adyen-endpoint/",
    "AuthHeaderName": "X-API-Key",
    "AuthHeaderValueTemplate": "{token}"
  }
}
```

`Adyen:ApiKey` is required. `ClientBaseUrl` should identify the Adyen service and environment your generated request builders target. `AuthHeaderName` defaults to `Authorization`, and `AuthHeaderValueTemplate` defaults to `Bearer {token}`.

For Adyen API-key authentication, set `AuthHeaderName` and `AuthHeaderValueTemplate` to the form required by that API, commonly `X-API-Key` and `{token}`.

## Registration

```csharp
using Soenneker.Adyen.OpenApiClientUtil.Registrars;

services.AddAdyenOpenApiClientUtilAsScoped();
```

The scoped utility uses a singleton HTTP-client provider, so disposing a scope does not remove the shared cached `HttpClient`. Use `AddAdyenOpenApiClientUtilAsSingleton()` when the generated client itself should also be shared application-wide.

## Usage

```csharp
using Soenneker.Adyen.OpenApiClient;
using Soenneker.Adyen.OpenApiClient.Models;
using Soenneker.Adyen.OpenApiClientUtil.Abstract;

public sealed class PaymentMethodService
{
    private readonly IAdyenOpenApiClientUtil _clientUtil;

    public PaymentMethodService(IAdyenOpenApiClientUtil clientUtil)
    {
        _clientUtil = clientUtil;
    }

    public async Task<PaymentMethodsResponse?> GetPaymentMethods(
        PaymentMethodsRequest request,
        CancellationToken cancellationToken = default)
    {
        AdyenOpenApiClient client = await _clientUtil.Get(cancellationToken);

        return await client.CheckoutServiceV72.PaymentMethods.PostAsync(
            request,
            cancellationToken: cancellationToken);
    }
}
```

## Behavior

- `Get()` lazily creates one generated client per utility instance and returns it on subsequent calls.
- Client creation reads configuration once. Later configuration changes do not rebuild the cached client.
- The authentication provider sends credentials only over HTTPS and pins itself to the first request host.
- Cancellation can stop initial client creation; it does not invalidate a client that has already been cached.
- Let the dependency-injection container dispose resolved utilities.
