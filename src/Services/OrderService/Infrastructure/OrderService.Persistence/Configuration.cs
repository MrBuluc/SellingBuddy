using Microsoft.Extensions.Configuration;

namespace OrderService.Persistence
{
    public class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\Infrastructure\\CatalogService.Persistence");
                configurationManager.AddJsonFile("PrivateInformations.json");
                return configurationManager.GetConnectionString("MsSQL");
            }
        }
    }
}
