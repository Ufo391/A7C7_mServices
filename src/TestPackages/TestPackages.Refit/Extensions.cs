using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace TestPackages.Refit
{
    public static class Extensions
    {
        public static IServiceCollection AddRefitClient<TService>(this ServiceCollection services, string baseUrl)
            where TService : class
        {
            services
                .AddRefitClient<TService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

            return services;
        }
    }
}