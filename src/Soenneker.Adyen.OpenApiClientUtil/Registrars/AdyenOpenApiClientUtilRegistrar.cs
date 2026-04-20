using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Adyen.HttpClients.Registrars;
using Soenneker.Adyen.OpenApiClientUtil.Abstract;

namespace Soenneker.Adyen.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class AdyenOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="AdyenOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAdyenOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddAdyenOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IAdyenOpenApiClientUtil, AdyenOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="AdyenOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAdyenOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddAdyenOpenApiHttpClientAsSingleton()
                .TryAddScoped<IAdyenOpenApiClientUtil, AdyenOpenApiClientUtil>();

        return services;
    }
}
