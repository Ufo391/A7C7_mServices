using MassTransit;
using MassTransit.Definition;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using System.Reflection;

namespace Shared.RabbitMq
{
    public static class Extensions
    {        
        public static void UseRabbitMq (this IServiceCollectionBusConfigurator container, string connectionStr)
        {
            var rabbitMq = new RabbitMqOptions(connectionStr);
            ConfigureContainer(container, rabbitMq);
        }

        public static void UseRabbitMq(this IServiceCollectionBusConfigurator container, string connectionStr, string applicationName)
        {
            var rabbitMq = new RabbitMqOptions(connectionStr,applicationName);
            ConfigureContainer(container, rabbitMq);

        }

        public static void UseRabbitMq(this IServiceCollectionBusConfigurator container, string connectionStr, Assembly executingAssembly)
        {
            var rabbitMq = new RabbitMqOptions(connectionStr, executingAssembly);
            ConfigureContainer(container, rabbitMq);
        }

        private static void ConfigureContainer(IServiceCollectionBusConfigurator container, RabbitMqOptions rabbitMq)
        {
            container.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMq.Host, rabbitMq.Port, rabbitMq.VHost, rabbitMq.ApplicationName, h =>
                {
                    h.Username(rabbitMq.Username);
                    h.Password(rabbitMq.Password);
                });
                cfg.ConfigureEndpoints(context, new DefaultEndpointNameFormatter(true));
            });
        }
    }
}