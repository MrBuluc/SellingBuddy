using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;

namespace Web.ApiGateway.Consul
{
    public class MyConsulServiceBuilder(Func<ConsulRegistryConfiguration> configurationFactory, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory) : DefaultConsulServiceBuilder(configurationFactory, clientFactory, loggerFactory)
    {
        protected override string GetDownstreamHost(ServiceEntry entry, Node node) => entry.Service.Address;
    }
}
